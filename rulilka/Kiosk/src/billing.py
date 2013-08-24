#!/usr/bin/env python
# -*- coding: utf-8 -*-

from configs import priceForHour
from SQLWorker import SQLWorker  
from repr_stuff import ReprName, ReprMoney
import time

import gtk

class Billing(object):
    def __init__(self):
        # may read from DB
        # price for sec
        self.price = priceForHour/3600.
        
    def CountMoney(self, note):
        'Here we count money (print bill, send to db, etc)'
        print int(note.money)

billQuery = "INSERT INTO bills (comp_id, time_used, money, session_end) VALUES (%d, %d, %d, %d)"

def PrintCheckForNote(note):
    note.printing = True
    sql = SQLWorker()
    sql.execute(billQuery % (note.id, note.time, note.money, int(time.time())))
    s = "Работа c ноутбуком %s завершена. %s" % (ReprName(note.name, use_markup=False), ReprMoney(note.money))
    msg = gtk.MessageDialog(flags=gtk.DIALOG_MODAL, buttons=gtk.BUTTONS_CLOSE, message_format=s)
    msg.run()
    msg.destroy()
    note.printing = False
    
    note.time = 0
    #print (note.id, note.time, note.money)

