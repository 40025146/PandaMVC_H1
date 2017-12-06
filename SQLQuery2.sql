select  客戶資料.客戶名稱,客戶清單.聯絡人數量,客戶清單.銀行數量 from(
select Count1.id,聯絡人數量,銀行數量 from (
	select 客戶資料.id, Count(*) as 聯絡人數量 from 客戶資料 inner join 客戶聯絡人 on 客戶聯絡人.客戶Id=客戶資料.Id group by 客戶資料.Id
) as Count1
left join (
	select 客戶資料.id, Count(*) as 銀行數量 from 客戶資料 inner join 客戶銀行資訊 on 客戶銀行資訊.客戶Id=客戶資料.Id group by 客戶資料.Id
) as Count2 
on Count1.id=Count2.id
) as 客戶清單 inner join 客戶資料 on 客戶資料.id=客戶清單.Id


