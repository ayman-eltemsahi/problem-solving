#!/bin/bash

set -e

clear
make

echo ""
echo "-----------------"
echo ""

time ./binary <../input.txt
# ./binary < ../input.txt > ../output.txt
