#!/bin/bash -e

NAME=$1

echo "File: $NAME"
# echo "Arguments: ${*: 2}"
printf "Starting ...\n"

rm -rf "$NAME.class" && javac "$NAME.java" && java "$NAME"
