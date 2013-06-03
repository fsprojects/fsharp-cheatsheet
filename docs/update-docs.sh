#!/bin/bash

# To use this script:
#  - Make sure you have `fsi` in the PATH
#  - Make sure you do not have any pending changes in `git status`
#  - Go to the root directory and run `./docs/update-docs.sh`

rm -rf .temp 
mkdir .temp
mkdir .temp/content
(cd .. && fsi build.fsx)
cp docs/*.html .temp
cp docs/content/* .temp/content
git checkout gh-pages
cp .temp/*.html .
cp .temp/content/* content/
git commit -a -m "Update generated documentation"
git push
git checkout master
rm -rf .temp 