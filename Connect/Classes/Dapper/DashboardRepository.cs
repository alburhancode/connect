using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Connect.Models.DashBoard;

namespace Connect.Classes.Dapper
{
	public class DashboardRepository : BaseRepository
	{
        internal IList<string> GetAllReplies(int messageId)
        {
            List<string> history;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    history = conn.Query<string>("GetAllReplies", new { messageId }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return history;
        }

        internal IList<DashboardMessage> GetAllMessages()
        {
            List<DashboardMessage> history;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    history = conn.Query<DashboardMessage>("GetAllMessages", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return history;
        }

        public DashboardMessage GetMessage(int id)
        {
            DashboardMessage individuals;
            var conn = Connection();
            try
            {
                using (conn)
                {
                    individuals = conn.QuerySingle<DashboardMessage>("GetMessage", new { MessageId = id },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
            return individuals;
        }

        internal void InsertMessage(DashboardMessage message)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("InsertMessage", new
                    {
                        message.Text,
                        message.Status
                    },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void DeleteMessage(int messageId)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("DeleteMessage", new
                    {
                        messageId
                    },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void UpdateMessage(DashboardMessage message)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("UpdateMessage", new
                    {
                        message.MessageId,
                        message.Text,
                        message.Status
                    },
                        commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                FinaliseConnection(conn);
            }
        }

        internal void AddReplyToMessage(DashboardMessage message)
        {
            var conn = Connection();

            try
            {
                using (conn)
                {
                    conn.Execute("AddReplyToMessage", new
                    {
                        message.MessageId,
                        Text = message.Reply
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