using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Connect.Classes.DataModels;

namespace Connect.Classes.Dapper
{
	public class PaymentRepository : BaseRepository
	{
        public List<IndividualGet> GetIndividualsWithPaymentVerifiedByBatch(string batch, string authority)
        {
            List<IndividualGet> individuals;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    individuals = conn.Query<IndividualGet>("dbo.individualswithpaymentverified_get", new { batch, authority },
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return individuals;
        }

        public void UpdateDateLastReminded(Guid id, DateTime dateLastReminded)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("UpdateDateLastReminded", new
						{
							id,
                            dateLastReminded
                    },
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

        internal void SavePanelGrading(IGradingLog gradingLog)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("panelgrade_save", new
                    {
                        gradingLog.Email,
                        gradingLog.Grade,
                        gradingLog.Comments
                    },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void SaveCeoGrading(IGradingLog gradingLog)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("ceograde_save", new
                    {
                        gradingLog.Email,
                        gradingLog.Grade,
                        gradingLog.Comments
                    },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }
    }
}