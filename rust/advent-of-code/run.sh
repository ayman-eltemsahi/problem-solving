#!/bin/bash

set -e

clear

echo ""
echo "-----------------"
echo ""

cargo run --release "$@"
