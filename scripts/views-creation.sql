--Run only once or run master-script
-- TODO: CHange to account/income breaktrough
CREATE VIEW budget_account_report
AS
(
SELECT e.account_id  as [id],
       a.name        as [name],
       MONTH(e.date) as [month],
       YEAR(e.date)  as [year],
       SUM(e.amount) as [total],
       e.user_id
FROM dbo.expense e
         INNER JOIN dbo.account a ON e.account_id = a.id
WHERE e.account_id IS NOT NULL
GROUP BY MONTH(e.date), YEAR(e.date), a.name, e.account_id, e.user_id
UNION
SELECT 0             as [id],
       'No account'  as [name],
       MONTH(e.date) as [month],
       YEAR(e.date)  as [year],
       SUM(e.amount) as [total],
       e.user_id
FROM dbo.expense e
WHERE e.account_id IS NULL
GROUP BY MONTH(e.date), YEAR(e.date), e.user_id
    )
go

-- TODO: CHange to account/income breaktrough

CREATE VIEW budget_category_report
AS
(
SELECT c.id          as [id],
       c.name        as [name],
       MONTH(e.date) as [month],
       YEAR(e.date)  as [year],
       SUM(e.amount) AS [total],
       c.user_id     AS [user_id]
FROM dbo.expense e
         INNER JOIN dbo.category c ON e.category_id = c.id
-- WHERE e.user_id = '123'
--   AND MONTH(e.date) = 6
--   AND YEAR(e.date) = 2020
GROUP BY c.id, c.name, MONTH(e.date), YEAR(e.date), c.user_id
UNION
SELECT -1              as [id],
       'Uncategorized' as [name],
       MONTH(e.date)   as [month],
       YEAR(e.date)    as [year],
       SUM(e.amount)   AS [total],
       e.user_id       AS [user_id]
FROM dbo.expense e
WHERE e.category_id IS NULL
GROUP BY MONTH(e.date), YEAR(e.date), e.user_id
UNION
SELECT 0              as [id],
       'Total Income' as [name],
       MONTH(i.date)  as [month],
       YEAR(i.date)   as [year],
       SUM(i.amount)  AS [total],
       i.user_id      AS [user_id]
FROM dbo.income i
-- WHERE i.user_id = '123'
--   AND MONTH(i.date) = 6
--   AND YEAR(i.date) = 2020
GROUP BY MONTH(i.date), YEAR(i.date), i.user_id)
go

