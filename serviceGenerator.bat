@echo off
REM Set variables
set SWAGGER_JSON_URL=https://localhost:7107/swagger/v1/swagger.json
set OUTPUT_DIR=swagger.json

REM Download Swagger JSON file
echo Downloading Swagger JSON file...
curl -o %OUTPUT_DIR% %SWAGGER_JSON_URL%

REM Notify user to upload to Swagger Editor
echo Swagger JSON downloaded to %OUTPUT_DIR%
echo Please upload this file to https://editor.swagger.io/, generate TypeScript Angular code, and replace the generated files in your Angular project.
pause
