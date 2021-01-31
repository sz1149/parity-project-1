source local/env.sh

dotnet test \
    /p:CollectCoverage=true \
    /p:Threshold=80 \
    /p:CoverletOutputFormat=lcov \
    /p:CoverletOutput="../../lcov.info"

