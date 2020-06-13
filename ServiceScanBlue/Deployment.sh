#!/bin/bash
cd /home/pi/Documents/ServiceScanBlue
chmod a+x scanService.sh
echo "[Unit]
Description= Scan les périphériques Bluetooth toutes les 15 sec

[Service]
Type=simple
ExecStart=/home/pi/Documents/ServiceScanBlue/scanService.sh

[Install]
WantedBy=multi-user.target
" > ./sserviceBlu.service
mv sserviceBlu.service /etc/systemd/system/serviceBlu.service
systemctl start serviceBlu.service
systemctl enable serviceBlu.service
