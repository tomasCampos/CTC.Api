namespace CTC.Application.Features.Revenue.UseCases
{
    internal static class RevenueSqlScripts
    {
        #region SELECT

        public static string SELECT_TRANSACTION_BY_REVENUE_ID = @"SELECT re.transaction_id FROM `heroku_3a06699194dd49a`.revenue re WHERE re.revenue_id = @revenue_id";

        public static string LIST_REVENUE_SELECT_STATEMENT = @"SELECT 
	                                                            re.revenue_id AS RevenueId,
                                                                re.client_id AS ClientId,
                                                                per.person_first_name AS ClientName,
                                                                re.transaction_id AS TransactionId,
                                                                tran.transaction_value AS `Value`,
                                                                tran.transaction_payment_date AS PaymentDate,
                                                                tran.transaction_observations AS Observation,
                                                                tran.category_id AS CategoryId,
                                                                cat.category_name AS CategoryName,
                                                                cc.cost_center_id AS CostCenterId,
                                                                cc.cost_center_name AS CostCenterName";

        public static string LIST_REVENUE_FROM_AND_JOIN_STATEMENT = @"FROM  
	                                                                    `heroku_3a06699194dd49a`.revenue re
                                                                    INNER JOIN
	                                                                    `heroku_3a06699194dd49a`.client cli ON re.client_id = cli.client_id
                                                                    INNER JOIN
	                                                                    `heroku_3a06699194dd49a`.person per ON cli.person_id = per.person_id
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.transaction tran ON re.transaction_id = tran.transaction_id
                                                                    LEFT JOIN
	                                                                    `heroku_3a06699194dd49a`.category cat ON tran.category_id = cat.category_id
                                                                    INNER JOIN
	                                                                    `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id";
        public static string SELECT_REVENUE_BY_ID = @"SELECT 
	                                                re.revenue_id AS RevenueId,
                                                    re.client_id AS ClientId,
                                                    per.person_first_name AS ClientName,
                                                    re.transaction_id AS TransactionId,
                                                    tran.transaction_value AS `Value`,
                                                    tran.transaction_payment_date AS PaymentDate,
                                                    tran.transaction_observations AS Observation,
                                                    tran.category_id AS CategoryId,
                                                    cat.category_name AS CategoryName,
                                                    cc.cost_center_id AS CostCenterId,
                                                    cc.cost_center_name AS CostCenterName
                                                FROM  
	                                                `heroku_3a06699194dd49a`.revenue re
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.client cli ON re.client_id = cli.client_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.person per ON cli.person_id = per.person_id
                                                INNER JOIN 
	                                                `heroku_3a06699194dd49a`.transaction tran ON re.transaction_id = tran.transaction_id
                                                LEFT JOIN
	                                                `heroku_3a06699194dd49a`.category cat ON tran.category_id = cat.category_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id
                                                WHERE re.revenue_id = @revenue_id";

        public static string SELECT_CLIENT_BY_COST_CENTER_ID = @"SELECT 
                                                                    c.client_id
                                                                FROM 
	                                                                cost_center c
                                                                WHERE 
                                                                    c.cost_center_id = @cost_center_id";
        #endregion

        #region INSERT

        public static string INSERT_REVENUE = @"INSERT INTO `heroku_3a06699194dd49a`.`revenue`
                                                (`revenue_id`,
                                                `client_id`,
                                                `transaction_id`)
                                                VALUES
                                                (@revenue_id,
                                                @client_id,
                                                @transaction_id);";

        #endregion

        #region UPDATE

        public static string UPDATE_REVENUE = @"UPDATE 
	                                                `heroku_3a06699194dd49a`.`revenue`
                                                SET
	                                                `client_id` = @client_id
                                                WHERE 
	                                                `revenue_id` = @revenue_id;";

        #endregion

        #region DELETE

        public static string DELETE_REVENUE = @"DELETE FROM `heroku_3a06699194dd49a`.`revenue` WHERE `heroku_3a06699194dd49a`.`revenue`.revenue_id = revenue_id;";

        #endregion
    }
}
