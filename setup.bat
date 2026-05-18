@echo off
REM Digital Platform - Automated Setup Script for Windows
REM This script sets up the entire development environment

setlocal enabledelayedexpansion

cls
echo.
echo ============================================================
echo    Digital Platform - Threat Intelligence Setup
echo    Automated Setup Script for Windows
echo ============================================================
echo.

REM ============================================
REM 1. Check Prerequisites
REM ============================================
echo Checking prerequisites...
echo.

REM Check Docker
docker --version >nul 2>&1
if errorlevel 1 (
    echo [ERROR] Docker is not installed
    echo Install from: https://docs.docker.com/desktop/install/windows-install/
    pause
    exit /b 1
)
for /f "tokens=*" %%i in ('docker --version') do set DOCKER_VERSION=%%i
echo [OK] %DOCKER_VERSION%

REM Check Docker Compose
docker-compose --version >nul 2>&1
if errorlevel 1 (
    echo [ERROR] Docker Compose is not installed
    echo Install from: https://docs.docker.com/compose/install/
    pause
    exit /b 1
)
for /f "tokens=*" %%i in ('docker-compose --version') do set COMPOSE_VERSION=%%i
echo [OK] %COMPOSE_VERSION%

REM Check .NET SDK
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo [ERROR] .NET SDK is not installed
    echo Install from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)
for /f "tokens=*" %%i in ('dotnet --version') do set DOTNET_VERSION=%%i
echo [OK] .NET %DOTNET_VERSION%

REM Check Git
git --version >nul 2>&1
if errorlevel 1 (
    echo [ERROR] Git is not installed
    echo Install from: https://git-scm.com/download/win
    pause
    exit /b 1
)
for /f "tokens=*" %%i in ('git --version') do set GIT_VERSION=%%i
echo [OK] %GIT_VERSION%

echo.
echo [SUCCESS] All prerequisites are installed!
echo.

REM ============================================
REM 2. Create .env if it doesn't exist
REM ============================================
echo Setting up environment variables...
if not exist .env (
    copy .env.example .env >nul
    echo [OK] Created .env file
) else (
    echo [WARNING] .env already exists
)
echo.

REM ============================================
REM 3. Start Docker Services
REM ============================================
echo Starting Docker services...
echo.

docker-compose ps >nul 2>&1
if not errorlevel 1 (
    echo [INFO] Restarting Docker containers...
    docker-compose restart
) else (
    echo [INFO] Starting Docker containers...
    docker-compose up -d
)

echo Waiting 10 seconds for containers to initialize...
timeout /t 10 /nobreak

echo.

REM ============================================
REM 4. Wait for services to be ready
REM ============================================
echo Waiting for services to be ready...
echo This may take 1-2 minutes...
echo.

echo Checking SQL Server...
set sql_ready=0
for /l %%i in (1,1,30) do (
    docker exec threat-intel-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "P@ssw0rd1234" -Q "SELECT 1" >nul 2>&1
    if errorlevel 0 (
        set sql_ready=1
        goto :sql_done
    )
    timeout /t 2 /nobreak >nul
)
:sql_done
if !sql_ready! equ 1 (
    echo [OK] SQL Server is ready
) else (
    echo [WARNING] SQL Server may not be ready yet
)

echo Checking Redis...
set redis_ready=0
for /l %%i in (1,1,30) do (
    docker exec threat-intel-redis redis-cli ping >nul 2>&1
    if errorlevel 0 (
        set redis_ready=1
        goto :redis_done
    )
    timeout /t 2 /nobreak >nul
)
:redis_done
if !redis_ready! equ 1 (
    echo [OK] Redis is ready
) else (
    echo [WARNING] Redis may not be ready yet
)

echo Checking RabbitMQ...
set rabbitmq_ready=0
for /l %%i in (1,1,30) do (
    curl -s -u guest:guest http://localhost:15672/api/whoami >nul 2>&1
    if errorlevel 0 (
        set rabbitmq_ready=1
        goto :rabbitmq_done
    )
    timeout /t 2 /nobreak >nul
)
:rabbitmq_done
if !rabbitmq_ready! equ 1 (
    echo [OK] RabbitMQ is ready
) else (
    echo [WARNING] RabbitMQ may not be ready yet
)

echo Checking Elasticsearch...
set es_ready=0
for /l %%i in (1,1,30) do (
    curl -s http://localhost:9200 >nul 2>&1
    if errorlevel 0 (
        set es_ready=1
        goto :es_done
    )
    timeout /t 2 /nobreak >nul
)
:es_done
if !es_ready! equ 1 (
    echo [OK] Elasticsearch is ready
) else (
    echo [WARNING] Elasticsearch may not be ready yet
)

echo.
echo [SUCCESS] Services are ready!
echo.

REM ============================================
REM 5. Display Information
REM ============================================
echo.
echo ============================================================
echo           Setup Complete! Services Ready
echo ============================================================
echo.
echo Available Services:
echo.
echo   API:              http://localhost:5000
echo   Swagger UI:       http://localhost:5000/swagger
echo   RabbitMQ:         http://localhost:15672
echo   Elasticsearch:    http://localhost:9200
echo   Kibana:           http://localhost:5601
echo   SQL Server:       localhost:1433
echo   Redis:            localhost:6379
echo.
echo Credentials:
echo.
echo   RabbitMQ User:       guest
echo   RabbitMQ Password:   guest
echo   SQL Server User:     sa
echo   SQL Server Password: P@ssw0rd1234
echo.
echo Next Steps:
echo.
echo   1. Navigate to src directory:
echo      cd src
echo.
echo   2. Restore NuGet packages:
echo      dotnet restore
echo.
echo   3. Build the solution:
echo      dotnet build
echo.
echo   4. Run the application:
echo      dotnet run
echo.
echo   5. Access the API at http://localhost:5000
echo.
echo Documentation:
echo.
echo   - Setup Guide:    .\SETUP_GUIDE.md
echo   - Security Info:  .\SECURITY.md
echo   - Environment:    .\.env
echo.
echo Useful Commands:
echo.
echo   View logs:              docker-compose logs -f
echo   Stop services:          docker-compose stop
echo   Start services:         docker-compose start
echo   Restart services:       docker-compose restart
echo   Remove all containers:  docker-compose down -v
echo.
echo Happy Coding!
echo.

REM ============================================
REM 6. Optional: Ask to continue with build
REM ============================================
set /p BUILD="Would you like to build and run the application now? (y/n): "
if /i "%BUILD%"=="y" (
    echo.
    echo Building application...
    cd src
    call dotnet restore
    if errorlevel 1 (
        echo [ERROR] Failed to restore packages
        pause
        exit /b 1
    )
    call dotnet build
    if errorlevel 1 (
        echo [ERROR] Failed to build solution
        pause
        exit /b 1
    )
    echo.
    echo Starting application...
    call dotnet run
)

pause
