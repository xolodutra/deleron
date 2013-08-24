#!/usr/bin/env python

import os

def Run(prog, args=[]):
    args.insert(0, prog)
    pid = os.fork()
    if not pid:
        try:
            os.execvp(prog, args)
        except:
            try:
                os.execv(prog, args)
            except:
                return 0
    return pid
