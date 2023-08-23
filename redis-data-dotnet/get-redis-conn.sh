REDIS_NAME="msdocs-redis-cache-anu"
 REDIS_KEY=$(az redis list-keys \
             --name "$REDIS_NAME" \
             --resource-group redis-cache-rg \
             --query primaryKey \
             --output tsv)

 echo "$REDIS_KEY"@"$REDIS_NAME".redis.cache.windows.net:6380?ssl=true
