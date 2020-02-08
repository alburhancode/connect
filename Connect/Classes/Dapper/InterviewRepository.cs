using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Connect.Classes.DataModels;

namespace Connect.Classes.Dapper
{
	public class InterviewRepository : BaseRepository
	{
		public void UpdateIndividualInterviewSlot(Guid id, int interviewSlotId)
		{
			var conn = Connection();

			try
			{
				using (conn)
				{
					conn.Execute("UpdateIndividualInterviewSlot", new
						{
							Id = id,
							InterviewSlotId = interviewSlotId
					},
						commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
		}

		//public List<InterviewSlot> GetAvailableInterviewSlots()
		//{
		//	List<InterviewSlot> records;
		//	var conn = Connection();
		//	try
		//	{
		//		using (conn)
		//		{
		//			records = conn.Query<InterviewSlot>("GetAvailableInterviewSlots", commandType: CommandType.StoredProcedure).ToList();
		//		}
		//	}
		//	finally
		//	{
		//		FinaliseConnection(conn);
		//	}
		//	return records;
		//}


        public List<InterviewSlot> GetAvailableInterviewSlots(string batch)
        {
            List<InterviewSlot> records;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    records = conn.Query<InterviewSlot>("GetAvailableInterviewSlots", new { batch },  commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return records;
        }

        public List<BatchTiming> GetAvailableBatchTimings(string batch)
        {
            List<BatchTiming> records;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    records = conn.Query<BatchTiming>("GetAvailableBatchTimings", new { batch }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return records;
        }

        //public bool SlotHasCapacity(Guid id, int interviewSlotId)
        //{
        //	bool slotHasCapacity;
        //	var conn = Connection();
        //	try
        //	{
        //		using (conn)
        //		{
        //			slotHasCapacity = conn.QuerySingle<bool>("SlotHasCapacity", new { Id = id, InterviewSlotId = interviewSlotId }, commandType: CommandType.StoredProcedure);
        //		}
        //	}
        //	finally
        //	{
        //		FinaliseConnection(conn);
        //	}
        //	return slotHasCapacity;
        //}

        public int GetSelectedInterviewSlot(Guid id)
		{
			int selectedInterviewSlot;
			var conn = Connection();
			try
			{
				using (conn)
				{
					selectedInterviewSlot = conn.QuerySingle<int>("GetSelectedInterviewSlot", new { Id = id }, commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return selectedInterviewSlot;
		}

        public int GetSelectedBatchTimingId(Guid id)
        {
            int selectedId;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    selectedId = conn.QuerySingle<int>("GetSelectedBatchTimingId", new { Id = id }, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return selectedId;
        }

        public string GetSelectedInterviewSlotById(int id)
		{
			string selectedInterviewSlot;
			var conn = Connection();
			try
			{
				using (conn)
				{
					selectedInterviewSlot = conn.QuerySingle<string>("GetSelectedInterviewSlotById", new { Id = id }, commandType: CommandType.StoredProcedure);
				}
			}
			finally
			{
				FinaliseConnection(conn);
			}
			return selectedInterviewSlot;
		}
	}
}