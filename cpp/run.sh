#!/bin/bash

set -e

clear
make
./binary < ../input
# ./binary < ../input > ../output.txt
