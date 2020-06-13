#!/bin/bash
while :
do
echo "scan start"
hcitool scan > /home/pi/Documents/ServiceScanBlue/blueDevices.txt
echo "scan done"
cd /home/pi/Documents/ServiceScanBlue/
./EnvoiApi.exe
sleep 10
done