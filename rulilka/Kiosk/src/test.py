#!/usr/bin/env python

from time import time, sleep
from ConfigParser import ConfigParser

''' Read test input and change some data '''

while (1):
    parser = ConfigParser()
    parser.read('test.ini')
    notes = parser.sections()
    for note in notes:
        parser.set(note, "last_ping", int(time()))
    with open('test.ini', 'w') as f:
        parser.write(f)
    sleep(1)