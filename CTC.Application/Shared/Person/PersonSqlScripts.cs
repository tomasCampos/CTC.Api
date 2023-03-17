using System.Reflection;

namespace CTC.Application.Shared.Person
{
    internal static class PersonSqlScripts
    {
        public static string INSERT_PERSON_SQL = @"INSERT INTO Person
                                                (`person_id`,
                                                `person_first_name`,
                                                `person_email`,
                                                `person_phone`,
                                                `person_document`)
                                                VALUES
                                                (@person_id,
                                                @person_first_name,
                                                @person_email,
                                                @person_phone,
                                                @person_document);
                                                ";

        public static string DELETE_PERSON_SQL = @"DELETE FROM `heroku_3a06699194dd49a`.person WHERE person_id = @person_id";

        private static string UPDATE_PERSON_SQL = @"UPDATE 
	                                                `heroku_3a06699194dd49a`.`person`
                                                SET
                                                    @@SET_STATEMENT@@
                                                WHERE
	                                                `person_id` = @person_id;";

        public static string GetUpdatePersonSql(PersonModel model)
        {
            var updatePersonSetStatement = "`person_first_name` = @person_first_name";

            if (!string.IsNullOrWhiteSpace(model.Email))
                updatePersonSetStatement += ",`person_email` = @person_email";

            if (!string.IsNullOrWhiteSpace(model.Phone))
                updatePersonSetStatement += ",`person_phone` = @person_phone";

            if (!string.IsNullOrWhiteSpace(model.Document))
                updatePersonSetStatement += ",`person_document` = @person_document";

            return UPDATE_PERSON_SQL.Replace("@@SET_STATEMENT@@", updatePersonSetStatement);
        }
    }
}
