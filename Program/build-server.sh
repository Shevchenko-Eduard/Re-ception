set -e

if [ -z "$CONFIGURATION_BUILD"]; then
    export CONFIGURATION_BUILD="Release"
fi

cd Presentations/WebApi

dotnet publish -c $CONFIGURATION_BUILD -o ../../output