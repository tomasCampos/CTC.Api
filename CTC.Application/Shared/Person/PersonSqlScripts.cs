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

        public static string UPDATE_PERSON_SQL = @"UPDATE 
	                                                `heroku_3a06699194dd49a`.`person`
                                                SET
	                                                `person_first_name` = @person_first_name,
	                                                `person_email` = @person_email,
	                                                `person_phone` = @person_phone,
	                                                `person_document` = @person_document
                                                WHERE
	                                                `person_id` = @person_id;";
    }
}
