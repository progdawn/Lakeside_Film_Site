select 'obj.' + column_name + ' = ' + case data_type
when 'int' then 'Convert.ToInt32(myReader["' + column_name + '"].ToString());'
when 'decimal' then 'Convert.ToDecimal(myReader["' + column_name + '"].ToString());'
when 'datetime' then 'Convert.ToDateTime(myReader["' + column_name + '"].ToString());'
else 'myReader["' + column_name + '"].ToString();'
end as prop
from INFORMATION_SCHEMA.COLUMNS
Where table_name = 'Members'
Order By ordinal_position