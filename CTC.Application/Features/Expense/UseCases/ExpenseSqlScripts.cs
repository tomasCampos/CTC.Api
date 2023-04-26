namespace CTC.Application.Features.Expense.UseCases
{
    internal static class ExpenseSqlScripts
    {
        #region SELECT

        public static string SELECT_TRANSACTION_BY_EXPENSE_ID = @"SELECT ex.transaction_id FROM `heroku_3a06699194dd49a`.expense ex WHERE ex.expense_id = @expense_id";

        public static string LIST_EXPENSE_SELECT_STATEMENT = @"SELECT 
	                                                            ex.expense_id AS ExpenseId,
                                                                ex.supplier_id AS SupplierId,
                                                                per.person_first_name AS SupplierName,
                                                                ex.transaction_id AS TransactionId,
                                                                tran.transaction_value AS `Value`,
                                                                tran.transaction_payment_date AS PaymentDate,
                                                                tran.transaction_observations AS Observation,
                                                                tran.category_id AS CategoryId,
                                                                cat.category_name AS CategoryName,
                                                                cc.cost_center_id AS CostCenterId,
                                                                cc.cost_center_name AS CostCenterName";

        public static string LIST_EXPENSE_FROM_AND_JOIN_STATEMENT = @"FROM  
	                                                                    `heroku_3a06699194dd49a`.expense ex
                                                                    INNER JOIN
	                                                                    `heroku_3a06699194dd49a`.supplier sup ON ex.supplier_id = sup.supplier_id
                                                                    INNER JOIN
	                                                                    `heroku_3a06699194dd49a`.person per ON sup.person_id = per.person_id
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.transaction tran ON ex.transaction_id = tran.transaction_id
                                                                    LEFT JOIN
	                                                                    `heroku_3a06699194dd49a`.category cat ON tran.category_id = cat.category_id
                                                                    INNER JOIN
	                                                                    `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id";

        public static string SELECT_EXPENSE_BY_ID = @"SELECT 
	                                                ex.expense_id AS ExpenseId,
                                                    ex.supplier_id AS SupplierId,
                                                    per.person_first_name AS SupplierName,
                                                    ex.transaction_id AS TransactionId,
                                                    tran.transaction_value AS `Value`,
                                                    tran.transaction_payment_date AS PaymentDate,
                                                    tran.transaction_observations AS Observation,
                                                    tran.category_id AS CategoryId,
                                                    cat.category_name AS CategoryName,
                                                    cc.cost_center_id AS CostCenterId,
                                                    cc.cost_center_name AS CostCenterName
                                                FROM  
	                                                `heroku_3a06699194dd49a`.expense ex
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.supplier sup ON ex.supplier_id = sup.supplier_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.person per ON sup.person_id = per.person_id
                                                INNER JOIN 
	                                                `heroku_3a06699194dd49a`.transaction tran ON ex.transaction_id = tran.transaction_id
                                                LEFT JOIN
	                                                `heroku_3a06699194dd49a`.category cat ON tran.category_id = cat.category_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id
                                                WHERE ex.expense_id = @expense_id";

        public static string VERIFY_IF_SUPPLIER_EXISTS = @"SELECT COUNT(*)
                                                    FROM
                                                        `heroku_3a06699194dd49a`.supplier s
                                                    WHERE
                                                        s.supplier_id = @supplier_id";

        #endregion

        #region INSERT

        public static string INSERT_EXPENSE = @"INSERT INTO `heroku_3a06699194dd49a`.`expense`
                                                (`expense_id`,
                                                `supplier_id`,
                                                `transaction_id`)
                                                VALUES
                                                (@expense_id,
                                                @supplier_id,
                                                @transaction_id);";

        #endregion

        #region UPDATE

        public static string UPDATE_EXPENSE = @"UPDATE 
	                                                `heroku_3a06699194dd49a`.`expense`
                                                SET
	                                                `supplier_id` = @supplier_id
                                                WHERE 
	                                                `expense_id` = @expense_id;";

        #endregion

        #region DELETE

        public static string DELETE_EXPENSE = @"DELETE FROM `heroku_3a06699194dd49a`.`expense` WHERE `heroku_3a06699194dd49a`.`expense`.expense_id = @expense_id;";

        #endregion
    }
}
