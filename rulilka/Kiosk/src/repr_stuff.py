#!/usr/bin/env python
# -*- coding: utf-8 -*-

def ReprBigStr(s, use_markup=True):
    if (use_markup):
        return '<span foreground="white" size="x-large">%s</span>' % (s,)
    return s

def ReprName(name, use_markup=True):
    return ReprBigStr(name, use_markup) 
    #return 'Ноутбук '+ name

def ReprSeconds(sec):
    ''' in HH:MM '''
    h, sec = divmod(sec, 3600)
    m, sec = divmod(sec, 60)
    
    return '%02d:%02d' % (h,m)

def ReprBatt(charge):
    return "Заряд: %d" % (charge,)

def ReprMoney(money):
    m = int(money)
    return "Сумма: %d руб." % (m,) 

