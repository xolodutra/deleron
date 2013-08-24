#!/usr/bin/env python

from NoteControl import NoteControl
#from lock import Locker
from UI import UI

def main():
    NC = NoteControl("test.ini")
    window = UI(NC)
    window.main()

if __name__ == '__main__':
    main()