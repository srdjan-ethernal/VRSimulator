#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
ENV_FILE="$ROOT_DIR/deploy/azure-vm/.env"

if [ ! -f "$ENV_FILE" ]; then
  echo "Missing $ENV_FILE. Copy deploy/azure-vm/.env.example to .env and fill in values."
  exit 1
fi

cd "$ROOT_DIR"
docker compose --env-file "$ENV_FILE" -f deploy/azure-vm/docker-compose.yml up -d --build
docker compose --env-file "$ENV_FILE" -f deploy/azure-vm/docker-compose.yml ps

