#!/bin/bash

LINE_PREFIX=":r "
LINE_REGEX="^(${LINE_PREFIX}.+)"
SE_REGEX="^(SE)"
FILE_RESET_DATABASE="./ResetDatabase.sql"

MSSQL_ARGS="-u sa -p Str0ngPass!"

TMP_MERGED_FILE="./ResetScript.sql"

SqlFiles=()
count=0

while IFS='' read -r line || [[ -n "$line" ]]; do
    if [[ "$line" =~ $LINE_REGEX ]]; then
        SqlFiles+=("${line#$LINE_PREFIX}")
        count=$((count + 1))
    fi
done < $FILE_RESET_DATABASE

echo "Found ${count} mssql commands."

for file in "${SqlFiles[@]}"; do
    while mapfile -t -n 10000 ary && ((${#ary[@]})); do
        # Some really weird character was giving us bugs when we were combining them using cat before...
        # So now we do this to get rid of that weird character.
        
        firstLine="${ary[0]#?}"
        if [[ "$firstLine" =~ $SE_REGEX ]]; then
            firstLine="U""${firstLine}"
        fi
        printf $'\n%s\n' "$firstLine"
        printf $'%s\n' "${ary[@]:1}"
    done < "$file"
done > "$TMP_MERGED_FILE"

echo "Execute the file ${TMP_MERGED_FILE} using azure data studio in mac by connecting to your local server."

echo "Finished combining ${count} sql scripts.";