#!/bin/zsh

cd `dirname $0`/..

#required environment variable:
echo _store_channel     : $_store_channel
echo _package_identifier: $_package_identifier
echo _bundle_identifier : $_bundle_identifier

if [ ! $_store_channel ]; then
    echo no input store channel
    exit 1
fi
if [ ! $_package_identifier ]; then
    echo no input package identifier
    exit 1
fi
if [ ! $_bundle_identifier ]; then
    echo no input bundle identifier
    exit 1
fi

declare _time_stamp=`date "+%Y-%m-%d_%H-%M-%S"`

#NOTE: unity will use "_output_dir" as output directory.
export _output_dir=BUILD/${_time_stamp}_${_store_channel}_${_package_identifier}_${_bundle_identifier}
declare _unity_log=$_output_dir/log.txt

"/Applications/Unity/Hub/Editor/2021.3.22f1c1/Unity.app/Contents/MacOS/Unity" \
    -executeMethod U3DMobileEditor.BuildProcess.Launch \
    -logFile $_unity_log \
    -batchmode \
    -quit

cat $_unity_log

if [ $? -ne 0 ]; then
    echo unity returns error
    exit 1
fi

exit 0
