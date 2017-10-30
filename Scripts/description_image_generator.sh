#!/bin/bash -e

if [ $# -ne 2 ]; then
    echo 'Usage'
    echo $0 inputDir outputDir
    echo 'You should redirect stdout to file, stdout contains file correspond(org,hash)'
    echo 'Bye'
    exit 1
fi

INPUTDIR=$1
OUTPUTDIR=$2

_WIDTH=512
_HEIGHT=384

for f in `ls ${INPUTDIR}/*`
do
    if [ `identify ${f} |cut -f3 -d' '|cut -f1 -d'x'` -lt ${_WIDTH} ] && [ `identify ${f} |cut -f3 -d' '|cut -f2 -d'x'` -lt ${_HEIGHT} ]; then
        convert -alpha remove ${f} -quiet tmp.jpg
    else
        convert -alpha remove -thumbnail ${_WIDTH}x${_HEIGHT} $f -quiet tmp.jpg
    fi

    newname=`openssl sha1 tmp.jpg | cut -f2 -d' '`
    mv tmp.jpg ${OUTPUTDIR}/$newname

    echo ${f##*/},,${newname}
done
