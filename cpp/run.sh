#!/bin/bash

set -e

clear
make

echo ""
echo "-----------------"
echo ""

./binary
# time ./binary <../input.txt
# ./binary < ../input.txt > ../output.txt
