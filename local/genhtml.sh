#!/bin/bash -e

# MacOsx:
# brew install lcov
# brew install genhtml
genhtml --output-directory lcov_html --rc lcov_branch_coverage=1 lcov.info
open lcov_html/index.html

