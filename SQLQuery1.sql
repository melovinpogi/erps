declare @from datetime,
		@to datetime,
		@name varchar(50),
		@entry date,
		@stime time,
		@etime time,
		@hr decimal(18,2),
		@user int,
		@week varchar(50),
		@from1 datetime,
		@to1 datetime,
		@week1 varchar(50)


declare @table table(userid int,hrs decimal(18,2),weeknumber varchar(50),ot_hrs decimal(18,2),pay_reg decimal(18,2),pay_ot decimal(18,2))

select	@from = cutoff_from,
		@to = cutoff_to,
		@week = week_number
from	timeclock_cutoff
where	'01/15/2018' between cutoff_from and cutoff_to

select	@from1 = cutoff_from,
		@to1 = cutoff_to,
		@week1 = week_number
from	timeclock_cutoff
where	'01/28/2018' between cutoff_from and cutoff_to

declare cur_payroll cursor for

select	distinct user_id
from	timeclock_timelogs

open cur_payroll
fetch cur_payroll into 
						@user

while @@fetch_status = 0
begin

	insert into @table
	select	@user,
			isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0),
			@week,
			case when isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) > 40
				then isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) -40
				else 0 end,
			case when isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) > 40
				then 40 * pay_per_hr
			else isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) * pay_per_hr end,
			case when isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) > 40
				then  (isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) -40) * ot_pay_rate
			else 0 end
	from	sys_user a

			inner join timeclock_timelogs b on
			b.user_id = a.id
	where	cast(entry_date as date) between @from and @to
			and user_id = @user
	group by pay_per_hr,
			ot_pay_rate
			
			union all

	select	@user,
			isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0),
			@week1,
			case when isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) > 40
				then isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) -40
				else 0 end,
			case when isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) > 40
				then 40 * pay_per_hr
			else isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) * pay_per_hr end,
			case when isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) > 40
				then  (isnull(sum(cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60),0) -40) * ot_pay_rate
			else 0 end
	from	sys_user a

			inner join timeclock_timelogs b on
			b.user_id = a.id
	where	cast(entry_date as date) between @from1 and @to1
			and user_id = @user
	group by pay_per_hr,
			ot_pay_rate



fetch cur_payroll into 
						@user
end

close cur_payroll

deallocate cur_payroll

select	*
from	@table
order by userid



select 0,
	userid,
	0,
	hrs,
	pay_reg,
	ot_hrs,
	pay_ot,
	hrs + ot_hrs as total_hrs, -- total hrs
	pay_reg + pay_ot as gross -- gross
from	@table

select	a.firstname,
			b.entry_date,
			b.start_time,
			b.end_time,
			cast(datediff(hour,start_time,end_time) as decimal(18,2)) % 60,
			a.id
	from	sys_user a

			inner join timeclock_timelogs b on
			b.user_id = a.id
	where	cast(entry_date as date) between '01/15/2018' and '01/28/2018'
			--and user_id = 22
--select	*
--from	timeclock_cutoff


--select	*
--from	timeclock_timelogs
