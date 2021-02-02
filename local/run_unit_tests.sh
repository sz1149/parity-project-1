source local/env.sh

dotnet test \
    /p:CollectCoverage=true \
    /p:Threshold=80 \
    /p:ThresholdType=line \
    /p:ThresholdType=method \
    /p:CoverletOutputFormat=lcov \
    /p:CoverletOutput="../../lcov.info"
