alter procedure pr_menu @user int
as
begin
	-- pr_menu 1
	select	d.module_group_name,
			c.module_name,
			c.link,
			d.icon as group_icon,
			c.id as module_id,
			d.id as group_id,
			c.seq
	from	sys_user a
	
			inner join sys_user_access b on
			b.user_id = a.id

			inner join sys_module c on
			c.id = b.module_id

			inner join sys_module_group d on
			d.id = c.module_group_id
	where	a.id = @user
			and c.show = 1
	order by isnull(c.seq,1000)
end

