#!/usr/bin/env python
import MySQLdb as DB
from configs import DBInfo

class ParsingError(Exception): pass

class SQLWorker(object):
    def __init__(self):
        try:
            self.db = DB.connect(host=DBInfo.db_host, 
                                 user = DBInfo.db_user, 
                                 passwd = DBInfo.db_pwd,
                                 db=DBInfo.db_name)
            self.curr = self.db.cursor()
        except  DB.Error, e:
            raise ParsingError(e)
            
    def execute(self, query):
        self.curr.execute(query)
        return self.curr.fetchall()
