FROM microsoft/mssql-server-linux

RUN mkdir /opt/mssql-scripts

COPY ./superhero.sql /opt/mssql-scripts

ENV MSSQL_SA_PASSWORD=SuperHer0esDbPa!s
ENV ACCEPT_EULA=Y

ENV PATH="/opt/mssql-tools/bin:${PATH}"

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'SuperHer0esDbPa!s' -i /opt/mssql-scripts/superhero.sql \
    && pkill sqlservr