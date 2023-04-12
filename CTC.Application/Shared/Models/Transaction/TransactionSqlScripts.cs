namespace CTC.Application.Shared.Models.Transaction
{
    internal static class TransactionSqlScripts
    {
        #region INSERT

        public static string INSERT_TRANSACTION = @"INSERT INTO `heroku_3a06699194dd49a`.`transaction`
                                            (`transaction_id`,
                                            `transaction_value`,
                                            `transaction_payment_date`,
                                            `transaction_observations`,
                                            `category_id`,
                                            `cost_center_id`)
                                            VALUES
                                            (@transaction_id,
                                            @transaction_value,
                                            @transaction_payment_date,
                                            @transaction_observations,
                                            @category_id,
                                            @cost_center_id);";

        #endregion

        #region UPDATE

        public static string UPDATE_TRANSACTION = @"UPDATE `heroku_3a06699194dd49a`.`transaction`
                                                    SET
                                                        `transaction_value` = @transaction_value,
                                                        `transaction_payment_date` = @transaction_payment_date,
                                                        `transaction_observations` = @transaction_observations,
                                                        `category_id` = @category_id,
                                                        `cost_center_id` = @cost_center_id
                                                    WHERE 
                                                        `transaction_id` = @transaction_id;";

        #endregion

        #region DELETE

        public static string DELETE_TRANSACTION = @"DELETE FROM `heroku_3a06699194dd49a`.`transaction` WHERE `transaction_id` = @transaction_id;";

        #endregion
    }
}
