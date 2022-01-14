FROM mongo:4.0.3

ENV MONGO_INITDB_ROOT_USERNAME usr_admin
ENV MONGO_INITDB_ROOT_PASSWORD admintanner
ENV MONGO_INITDB_DATABASE admin

ADD mongodb-data/mongo-init.js /docker-entrypoint-initdb.d/