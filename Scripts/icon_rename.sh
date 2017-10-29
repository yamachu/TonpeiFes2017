#!/bin/bash -e

if [ $# -ne 2 ]; then
    echo 'Usage'
    echo $0 inputDir outputDir
    echo 'Bye'
    exit 1
fi

INPUTDIR=$1
OUTPUTDIR=$2

# Kill normal-whitespace, Replace it to underscore
python3 -c "
import os
import glob

for f in glob.glob('${INPUTDIR}' + '/*'):
    newfile = f.replace(' ', '')
    os.rename(f, newfile)
    fileName = os.path.basename(newfile)
    pathName = os.path.dirname(newfile)
    os.rename(newfile, '${OUTPUTDIR}' + '/icon_' + fileName)
"