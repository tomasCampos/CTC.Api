namespace CTC.Application.Shared.Person
{
    internal static class PersonSqlScripts
    {
        public static string InsertPersonSql = @"INSERT INTO Person
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
    }
}
