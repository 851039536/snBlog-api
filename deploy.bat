#!/bin/bash

# 停止并移除旧的容器
docker-compose down

# 构建并启动服务
docker-compose up -d --build
