using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using HealthTracker181486.Models;

namespace HealthTracker181486.Data
{
    public class HealthDatabase
    {
        const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        readonly string _databasePath;
        readonly SemaphoreSlim _initLock = new(1, 1);
        SQLiteAsyncConnection? _connection;

        public HealthDatabase(string databasePath)
        {
            _databasePath = databasePath;
        }

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_connection is not null)
                return _connection;

            await _initLock.WaitAsync();
            try
            {
                if(_connection is not null)
                    return _connection;

                var connection = new SQLiteAsyncConnection(_databasePath, Flags);

                await connection.CreateTableAsync(CreateFlags.None, typeof(User), typeof(ExerciseType), typeof(ExerciseSession), typeof(ActivityInterval), typeof(DailySummary), typeof(SleepSession), typeof(HealthMeasurement), typeof(Goal));

                await DataSeeder.SeedAsync(connection);

                _connection = connection;
                return _connection;
            }
            finally
            {
                _initLock.Release();
            }
        }
    }
}
