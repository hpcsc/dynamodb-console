#!/bin/bash

CONTAINER_NAME=local-dynamodb

[[ $(docker ps -a --filter "name=^/${CONTAINER_NAME}$" --format '{{.Names}}') == ${CONTAINER_NAME} ]] &&
docker start ${CONTAINER_NAME} ||
docker run -d --name ${CONTAINER_NAME} -p 8000:8000 amazon/dynamodb-local:1.11.477 -jar DynamoDBLocal.jar -sharedDb
