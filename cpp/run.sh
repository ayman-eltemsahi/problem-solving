#!/bin/bash

set -e

clear
make
time ./binary < ../input
# ./binary < ../input > ../output.txt
