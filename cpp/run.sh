#!/bin/bash

set -e

clear
make

echo ""
echo "-----------------"
echo ""

time ./binary <../input
# ./binary < ../input > ../output.txt
