#!/usr/bin/env python

#from LogParser import LogParser
from billing import Billing, PrintCheckForNote

from SQLParser import LogParser
from SQLWorker import SQLWorker
#from LogParser import LogParser

from datetime import datetime, timedelta
from run import Run 

from configs import debug, availableExpCnt, logInterval

def PingIP(ip, cnt=1):
    if (cnt != 0):
        Run('ping', ['-c', str(cnt), ip])
    else:
        Run('ping', [ip])

def GetTimeDeltaInSec(t):
    sec_in_day = 3600*24
    delta = datetime.now() - t 
    res = delta.days*sec_in_day + delta.seconds
    if debug:
        print "GetTimeDelta. In:", t, "Delta:",delta, "res:", res
    return res

class Note(object):
    def __init__(self, name, billing, log):
        self.name = name
        self.log = log
        # default values
        # is note on flag
        self.works = True
        # is note in use flag
        self.inuse = False
        # time meter
        self.oldtime = 0
        self.time = 0
        # money meter
        self.money = 0
        self.billing = billing
        # battery meter 
        self.batt = 100
        self.ip = ''
        try:
            self.worker = SQLWorker()
        except:
            pass
        self.broken = False
        self.turnedOn = True
        self.get = False
        self.ignoreTurnOff = False
        self.ignore_ping = datetime.now()
        
        self.first_time = True
        self.printing = False
        
    def Update(self, stateDict):
        self.id = stateDict['id']
        
        if(self.first_time and stateDict['state'] == 1):
            self.SetUse(False)
            return
        self.first_time = False
        if (self.printing): return
        
        if ((stateDict['state'] == 2 or stateDict['state'] == 0) and self.inuse):
            # client turned off the computer
            self.get = True
            self.SetUse(False, status=0)
            PrintCheckForNote(self)
            
        if (stateDict['state'] == 1):
            # inuse
            self.inuse = True
        #if (stateDict['state'] == 0):
            # inuse
        #    self.inuse = False

        self.broken = stateDict['broken']
        #if self.broken: return
        self.last_ping = stateDict['last_ping']
        if (self.oldtime == 0):
            self.oldtime = self.last_ping
        if (debug):
            print self.last_ping, self.oldtime
            print type(self.last_ping), type(self.oldtime)
        
        expTimeInSec = GetTimeDeltaInSec(self.oldtime)
        if (debug):
            print 'expTimeInSec', expTimeInSec

        #if self.broken:
        #    if (expTimeInSec < logInterval*availableExpCnt):
        #        self.SetBroken(False)
        
        if self.inuse:
            if debug:
                print self.last_ping, self.oldtime
            #if (self.last_ping != self.oldtimeself.get = True):
            if (True):
                self.time += min (expTimeInSec, logInterval)
                #self.time += expTimeInSec
                self.oldtime = datetime.now() 
                self.money = self.billing.price * (int(self.time/60)*60)
        else:
            if (self.time != 0):
                # using was changed externally
                PrintCheckForNote(self)
                self.time = 0
            self.oldtime = self.last_ping

        if (GetTimeDeltaInSec(self.last_ping) > logInterval*availableExpCnt):
            self.turnedOn = False
        elif ( not self.IsPingIgnored()) :
            self.turnedOn = True
            self.ignoreTurnOff = False
            if (self.broken):
                self.SetBroken(False)
                if debug: print "Timedelta:", GetTimeDeltaInSec(self.last_ping)
        self.batt = min([int(stateDict['batt']), 100])
        self.ip = stateDict['ip']
        PingIP(self.ip)
        self.id = stateDict['id']
        
        
    def CopyState(self, otherNote):
        # default values
        # is note on flag
        # is note in use flag
        # time meter
        self.oldtime = otherNote.oldtime
        self.time = otherNote.time
        # money meter
        self.money = otherNote.money
        otherNote.time  = 0
        otherNote.money
        # battery meter
        
    def SetUse(self, use, status=None):
        self.oldtime = self.log.SetUseReturnOldTime(self.id, use, status)
        self.inuse = use
        self.IgnorePing()
        
    def SetBroken(self, broken):
        self.log.SetBroken(self.id, broken)
        self.SetUse(False)
        self.broken = broken
        self.IgnorePing()

    def IgnorePing(self):
        self.ignore_ping = datetime.now() + timedelta(seconds=logInterval*availableExpCnt)

    def IsPingIgnored(self):
        if debug:
            print datetime.now(), self.ignore_ping, self.ignore_ping > datetime.now()
        return self.ignore_ping > datetime.now()

class NoteControl(object):
    def __init__(self, filename):
        self.log = LogParser()
        self.log.parse()
        self.noteNames = self.log.noteNames[:]
        self.noteNames.sort()
        self.notes = {}
        for name in self.noteNames:
            self.notes[name] = Note(name, Billing(), self.log)
        self.Update()
        
    def Update(self):
        self.log.parse()
        for name in set(self.log.noteNames).difference(set(self.noteNames)):
            self.notes[name] = Note(name, Billing(), self.log)
            
        for name in set(self.noteNames).difference(set(self.log.noteNames)):
            del self.notes[name]
            
        self.noteNames = self.log.noteNames[:]
        self.noteNames.sort()
        for noteName, note in self.notes.items():
            note.Update(self.log.notes[noteName])
            if debug:
                print self.log.notes[noteName]

