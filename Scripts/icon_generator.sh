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

for f in `ls ${INPUTDIR}/*`
do
    convert $f -alpha remove -thumbnail 120x120 -background white -gravity center -extent 120x120 -quiet tmp.png
    newname=`openssl sha1 tmp.png | cut -f2 -d' '`
    mv tmp.png ${OUTPUTDIR}/$newname

    echo ${f##*/},${newname},
done
