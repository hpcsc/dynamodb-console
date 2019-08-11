#!/bin/sh

TABLE_STATUS=$(aws dynamodb list-tables --endpoint-url http://dynamodb:8000 --region ap-southeast-2 --output json | jq -r '.TableNames[]' | grep Music)
if [[ -z "${TABLE_STATUS}" ]]; then
    aws dynamodb create-table \
        --table-name Music \
        --attribute-definitions \
            AttributeName=Artist,AttributeType=S \
            AttributeName=SongTitle,AttributeType=S \
        --key-schema \
            AttributeName=Artist,KeyType=HASH \
            AttributeName=SongTitle,KeyType=RANGE \
        --provisioned-throughput \
            ReadCapacityUnits=10,WriteCapacityUnits=5 \
        --endpoint-url \
            http://dynamodb:8000 \
        --region \
            ap-southeast-2
fi

aws dynamodb put-item \
    --table-name Music  \
    --item \
        '{"Artist": {"S": "No One You Know"}, "SongTitle": {"S": "Call Me Today"}, "AlbumTitle": {"S": "Somewhat Famous"}, "Awards": {"N": "1"}}' \
    --endpoint-url \
        http://dynamodb:8000 \
    --region \
        ap-southeast-2

aws dynamodb put-item \
    --table-name Music \
    --item \
        '{"Artist": {"S": "Acme Band"}, "SongTitle": {"S": "Happy Day"}, "AlbumTitle": {"S": "Songs About Life"}, "Awards": {"N": "10"} }' \
    --endpoint-url \
        http://dynamodb:8000 \
    --region \
        ap-southeast-2
