#!/bin/bash

# Digital Platform - Automated Setup Script
# For Linux and macOS

set -e

echo ""
echo "============================================================"
echo "   Digital Platform - Threat Intelligence Setup"
echo "   Automated Setup Script for Linux/macOS"
echo "============================================================"
echo ""

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# ============================================
# 1. Check Prerequisites
# ============================================
echo "Checking prerequisites..."
echo ""

# Check Docker
if ! command -v docker &> /dev/null; then
    echo -e "${RED}[ERROR] Docker is not installed${NC}"
    echo "Install from: https://docs.docker.com/install/"
    exit 1
fi
DOCKER_VERSION=$(docker --version)
echo -e "${GREEN}[OK]${NC} $DOCKER_VERSION"

# Check Docker Compose
if ! command -v docker-compose &> /dev/null; then
    echo -e "${RED}[ERROR] Docker Compose is not installed${NC}"
    echo "Install from: https://docs.docker.com/compose/install/"
    exit 1
fi
COMPOSE_VERSION=$(docker-compose --version)
echo -e "${GREEN}[OK]${NC} $COMPOSE_VERSION"

# Check .NET SDK
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}[ERROR] .NET SDK is not installed${NC}"
    echo "Install from: https://dotnet.microsoft.com/download"
    exit 1
fi
DOTNET_VERSION=$(dotnet --version)
echo -e "${GREEN}[OK]${NC} .NET $DOTNET_VERSION"

# Check Git
if ! command -v git &> /dev/null; then
    echo -e "${RED}[ERROR] Git is not installed${NC}"
    echo "Install from: https://git-scm.com/downloads"
    exit 1
fi
GIT_VERSION=$(git --version)
echo -e "${GREEN}[OK]${NC} $GIT_VERSION"

echo ""
echo -e "${GREEN}[SUCCESS] All prerequisites are installed!${NC}"
echo ""

# ============================================
# 2. Create .env if it doesn't exist
# ============================================
echo "Setting up environment variables..."
if [ ! -f .env ]; then
    cp .env.example .env
    echo -e "${GREEN}[OK]${NC} Created .env file"
else
    echo -e "${YELLOW}[WARNING]${NC} .env already exists"
fi
echo ""

# ============================================
# 3. Start Docker Services
# ============================================
echo "Starting Docker services..."
echo ""

# Check if containers are already running
if docker-compose ps 2>/dev/null | grep -q "Up"; then
    echo -e "${YELLOW}[INFO]${NC} Restarting Docker containers..."
    docker-compose restart
else
    echo -e "${YELLOW}[INFO]${NC} Starting Docker containers..."
    docker-compose up -d
fi

echo "Waiting 10 seconds for containers to initialize..."
sleep 10

echo ""

# ============================================
# 4. Wait for services to be ready
# ============================================
echo "Waiting for services to be ready..."
echo "This may take 1-2 minutes..."
echo ""

# Function to wait for a service
wait_for_service() {
    local service=$1
    local check_command=$2
    local max_attempts=30
    local attempt=1

    echo -n "Checking $service... "
    while [ $attempt -le $max_attempts ]; do
        if eval "$check_command" > /dev/null 2>&1; then
            echo -e "${GREEN}[OK]${NC}"
            return 0
        fi
        echo -n "."
        sleep 2
        ((attempt++))
    done
    echo -e "${YELLOW}[WARNING]${NC} $service may not be ready yet"
    return 1
}

# Check SQL Server
wait_for_service "SQL Server" \
    "docker exec threat-intel-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'P@ssw0rd1234' -Q 'SELECT 1'"

# Check Redis
wait_for_service "Redis" \
    "docker exec threat-intel-redis redis-cli ping"

# Check RabbitMQ
wait_for_service "RabbitMQ" \
    "curl -s -u guest:guest http://localhost:15672/api/whoami"

# Check Elasticsearch
wait_for_service "Elasticsearch" \
    "curl -s http://localhost:9200"

echo ""
echo -e "${GREEN}[SUCCESS] Services are ready!${NC}"
echo ""

# ============================================
# 5. Display Information
# ============================================
cat << 'EOF'

============================================================
           Setup Complete! Services Ready
============================================================

Available Services:

  API:              http://localhost:5000
  Swagger UI:       http://localhost:5000/swagger
  RabbitMQ:         http://localhost:15672
  Elasticsearch:    http://localhost:9200
  Kibana:           http://localhost:5601
  SQL Server:       localhost:1433
  Redis:            localhost:6379

Credentials:

  RabbitMQ User:       guest
  RabbitMQ Password:   guest
  SQL Server User:     sa
  SQL Server Password: P@ssw0rd1234

Next Steps:

  1. Navigate to src directory:
     cd src

  2. Restore NuGet packages:
     dotnet restore

  3. Build the solution:
     dotnet build

  4. Run the application:
     dotnet run

  5. Access the API at http://localhost:5000

Documentation:

  - Setup Guide:    ./SETUP_GUIDE.md
  - Security Info:  ./SECURITY.md
  - Environment:    ./.env

Useful Commands:

  View logs:              docker-compose logs -f
  Stop services:          docker-compose stop
  Start services:         docker-compose start
  Restart services:       docker-compose restart
  Remove all containers:  docker-compose down -v

============================================================
Happy Coding!
============================================================

EOF

# ============================================
# 6. Optional: Ask to continue with build
# ============================================
read -p "Would you like to build and run the application now? (y/n): " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo ""
    echo "Building application..."
    cd src
    
    echo "Restoring packages..."
    dotnet restore
    if [ $? -ne 0 ]; then
        echo -e "${RED}[ERROR] Failed to restore packages${NC}"
        exit 1
    fi
    
    echo "Building solution..."
    dotnet build
    if [ $? -ne 0 ]; then
        echo -e "${RED}[ERROR] Failed to build solution${NC}"
        exit 1
    fi
    
    echo ""
    echo "Starting application..."
    dotnet run
fi
