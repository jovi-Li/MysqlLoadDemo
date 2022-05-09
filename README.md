# MysqlLoadDemo

## 测试阿里云实例批量插入数据

### 3个关键点

1. 服务器（rds)配置local_infile =on 
2. 连接字符串添加:AllowLoadLocalInfile=true;
3. MySqlBulkLoader方法配置：Local = true
