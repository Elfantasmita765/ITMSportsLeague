using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace SportsLeague.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(LeagueDbContext context)
        {
            // Solo ejecutar si no hay equipos (BD vacía)
            if (await context.Teams.AnyAsync()) return;

            // ═══ 1. EQUIPOS (Liga BetPlay 2026) ═══
            var teams = new List<Team>
        {
            new() { Name="Atlético Nacional", City="Medellín", Stadium="Atanasio Girardot" },
            new() { Name="Independiente Medellín", City="Medellín", Stadium="Atanasio Girardot" },
            new() { Name="América de Cali", City="Cali", Stadium="Pascual Guerrero" },
            new() { Name="Deportivo Cali", City="Cali", Stadium="Deportivo Cali" },
            new() { Name="Junior FC", City="Barranquilla", Stadium="Metropolitano" },
            new() { Name="Millonarios FC", City="Bogotá", Stadium="El Campín" },
            new() { Name="Independiente Santa Fe", City="Bogotá", Stadium="El Campín" },
            new() { Name="Deportes Tolima", City="Ibagué", Stadium="Manuel Murillo Toro" },
            new() { Name="Atlético Bucaramanga", City="Bucaramanga", Stadium="Alfonso López" },
            new() { Name="Once Caldas", City="Manizales", Stadium="Palogrande" },
            new() { Name="Deportivo Pasto", City="Pasto", Stadium="Departamental Libertad" },
            new() { Name="Deportivo Pereira", City="Pereira", Stadium="Hernán Ramírez Villegas" },
            new() { Name="Águilas Doradas", City="Rionegro", Stadium="Alberto Grisales" },
            new() { Name="Boyacá Chicó FC", City="Tunja", Stadium="La Independencia" },
            new() { Name="Jaguares de Córdoba", City="Montería", Stadium="Jaraguay" },
            new() { Name="Alianza Valledupar FC", City="Valledupar", Stadium="Armando Maestre" },
            new() { Name="Fortaleza FC", City="Bogotá", Stadium="Metropolitano de Techo" },
            new() { Name="Llaneros FC", City="Villavicencio", Stadium="Bello Horizonte" },
            new() { Name="Cúcuta Deportivo", City="Cúcuta", Stadium="General Santander" },
            new() { Name="Internacional de Bogotá", City="Bogotá", Stadium="Metropolitano de Techo" },
        };

            context.Teams.AddRange(teams);
            await context.SaveChangesAsync();

            // ═══ 2. JUGADORES (12 por equipo = 240 total) ═══
            var playersData = new (string First, string Last, PlayerPosition Pos, int Number)[][]
            {
            // 1. Atlético Nacional
            new[] {
                ("David", "Ospina", PlayerPosition.Goalkeeper, 1),
                ("William", "Tesillo", PlayerPosition.Defender, 3),
                ("Edwin", "Cardona", PlayerPosition.Midfielder, 10),
                ("Alfredo", "Morelos", PlayerPosition.Forward, 9),
                ("Kevin", "Mier", PlayerPosition.Goalkeeper, 12),
                ("Cristian", "Zapata", PlayerPosition.Defender, 13),
                ("Jarlan", "Barrera", PlayerPosition.Midfielder, 14),
                ("Dorlan", "Pabón", PlayerPosition.Forward, 15), 
                ("Yerson", "Candelo", PlayerPosition.Defender, 16),
                ("Nelson", "Deossa", PlayerPosition.Midfielder, 17),
                ("Jefferson", "Duque", PlayerPosition.Forward, 18),
                ("Sergio", "Mosquera", PlayerPosition.Defender, 19),
            },
            // 2. Independiente Medellín
            new[] {
                ("Salvador", "Ichazo", PlayerPosition.Goalkeeper, 1),
                ("Andrés", "Cadavid", PlayerPosition.Defender, 4),
                ("Adrián", "Arregui", PlayerPosition.Midfielder, 5),
                ("Luciano", "Pons", PlayerPosition.Forward, 9),
                ("Eder", "Chaux", PlayerPosition.Goalkeeper, 11),
                ("José", "Ortiz", PlayerPosition.Defender, 12),
                ("Daniel", "Torres", PlayerPosition.Midfielder, 13),
                ("Brayan", "León", PlayerPosition.Forward, 14),
                ("Jimer", "Fory", PlayerPosition.Defender, 15),
                ("Miguel", "Monsalve", PlayerPosition.Midfielder, 16),
                ("Edwuin", "Cetré", PlayerPosition.Forward, 17),
                ("Víctor", "Moreno", PlayerPosition.Defender, 18),
            },
            // 3. América de Cali
            new[] {
                ("Joel", "Graterol", PlayerPosition.Goalkeeper, 1),
                ("Jorge", "Segura", PlayerPosition.Defender, 3),
                ("Rodrigo", "Ureña", PlayerPosition.Midfielder, 8),
                ("Adrián", "Ramos", PlayerPosition.Forward, 9),
                ("Diego", "Novoa", PlayerPosition.Goalkeeper, 11),
                ("Brayan", "Medina", PlayerPosition.Defender, 12),
                ("Franco", "Leys", PlayerPosition.Midfielder, 13),
                ("Michael", "Barrios", PlayerPosition.Forward, 14),
                ("Cristian", "Arrieta", PlayerPosition.Defender, 15),
                ("Juan", "Quintero", PlayerPosition.Midfielder, 16),
                ("Ever", "Valencia", PlayerPosition.Forward, 17),
                ("Daniel", "Bocanegra", PlayerPosition.Defender, 18),
            },
            // 4. Deportivo Cali
            new[] {
                ("Pedro", "Gallese", PlayerPosition.Goalkeeper, 1),
                ("Fernando", "Álvarez", PlayerPosition.Defender, 4),
                ("Kevin", "Velasco", PlayerPosition.Midfielder, 10),
                ("Juan", "Dinenno", PlayerPosition.Forward, 9),
                ("Humberto", "Acevedo", PlayerPosition.Goalkeeper, 11),
                ("Germán", "Mera", PlayerPosition.Defender, 12),
                ("Fabry", "Castro", PlayerPosition.Midfielder, 13),
                ("Luis", "Sandoval", PlayerPosition.Forward, 14),
                ("Jhon", "Vásquez", PlayerPosition.Defender, 15),
                ("Andrés", "Colorado", PlayerPosition.Midfielder, 16),
                ("Teófilo", "Gutiérrez", PlayerPosition.Forward, 17),
                ("Kevin", "Riascos", PlayerPosition.Defender, 18),
            },
            // 5. Junior FC
            new[] {
                ("Mauro", "Silveira", PlayerPosition.Goalkeeper, 1),
                ("Edwin", "Herrera", PlayerPosition.Defender, 4),
                ("Fabián", "Ángel", PlayerPosition.Midfielder, 8),
                ("Carlos", "Bacca", PlayerPosition.Forward, 7),
                ("Jefferson", "Martínez", PlayerPosition.Goalkeeper, 11),
                ("Jermein", "Peña", PlayerPosition.Defender, 12),
                ("Didier", "Moreno", PlayerPosition.Midfielder, 13),
                ("Yimmi", "Chará", PlayerPosition.Forward, 14),
                ("Gabriel", "Fuentes", PlayerPosition.Defender, 15),
                ("Víctor", "Cantillo", PlayerPosition.Midfielder, 16),
                ("Marco", "Pérez", PlayerPosition.Forward, 18),
                ("Emanuel", "Olivera", PlayerPosition.Defender, 19),
            },
            // 6. Millonarios FC
            new[] {
                ("Guillermo", "De Amores", PlayerPosition.Goalkeeper, 1),
                ("Omar", "Bertel", PlayerPosition.Defender, 4),
                ("Daniel", "Cataño", PlayerPosition.Midfielder, 10),
                ("Leonardo", "Castro", PlayerPosition.Forward, 9),
                ("Álvaro", "Montero", PlayerPosition.Goalkeeper, 11),
                ("Andrés", "Llinás", PlayerPosition.Defender, 12),
                ("Larry", "Vásquez", PlayerPosition.Midfielder, 13),
                ("Fernando", "Uribe", PlayerPosition.Forward, 14),
                ("Juan Pablo", "Vargas", PlayerPosition.Defender, 15),
                ("David", "Silva", PlayerPosition.Midfielder, 16),
                ("Santiago", "Giordana", PlayerPosition.Forward, 17),
                ("Jorge", "Arias", PlayerPosition.Defender, 18),
            },
            // 7. Independiente Santa Fe
            new[] {
                ("Leandro", "Castellanos", PlayerPosition.Goalkeeper, 1),
                ("Elvis", "Mosquera", PlayerPosition.Defender, 3),
                ("Daniel", "Giraldo", PlayerPosition.Midfielder, 5),
                ("Hugo", "Rodallega", PlayerPosition.Forward, 9),
                ("Antony", "Silva", PlayerPosition.Goalkeeper, 11),
                ("Marcelo", "Ortiz", PlayerPosition.Defender, 12),
                ("Jhojan", "Torres", PlayerPosition.Midfielder, 13),
                ("Wilson", "Morelo", PlayerPosition.Forward, 14),
                ("Dairon", "Mosquera", PlayerPosition.Defender, 15),
                ("Christian", "Marrugo", PlayerPosition.Midfielder, 16),
                ("Agustín", "Rodríguez", PlayerPosition.Forward, 17),
                ("José", "Aja", PlayerPosition.Defender, 18),
            },
            // 8. Deportes Tolima
            new[] {
                ("William", "Cuesta", PlayerPosition.Goalkeeper, 1),
                ("Jersson", "González", PlayerPosition.Defender, 3),
                ("Junior", "Hernández", PlayerPosition.Midfielder, 10),
                ("Tatay", "Torres", PlayerPosition.Forward, 9),
                ("Cristopher", "Fiermarín", PlayerPosition.Goalkeeper, 11),
                ("Julián", "Quiñones", PlayerPosition.Defender, 12),
                ("Yeison", "Guzmán", PlayerPosition.Midfielder, 13),
                ("Diego", "Herazo", PlayerPosition.Forward, 14),
                ("Anderson", "Angulo", PlayerPosition.Defender, 15),
                ("Brayan", "Rovira", PlayerPosition.Midfielder, 16),
                ("Facundo", "Boné", PlayerPosition.Forward, 17),
                ("Marlon", "Torres", PlayerPosition.Defender, 18),
            },
            // 9. Atlético Bucaramanga
            new[] {
                ("Juan Camilo", "Chaverra", PlayerPosition.Goalkeeper, 1),
                ("José", "Ortiz", PlayerPosition.Defender, 4),
                ("Sherman", "Cárdenas", PlayerPosition.Midfielder, 10),
                ("Sebastián", "Pons", PlayerPosition.Forward, 9),
                ("Aldair", "Quintana", PlayerPosition.Goalkeeper, 11),
                ("Cristian", "Zapara", PlayerPosition.Defender, 12),
                ("Fabián", "Sambueza", PlayerPosition.Midfielder, 13),
                ("Gonzalo", "Lencina", PlayerPosition.Forward, 14),
                ("Carlos", "Romaña", PlayerPosition.Defender, 15),
                ("Jhon", "Flores", PlayerPosition.Midfielder, 16),
                ("Michael", "Rangel", PlayerPosition.Forward, 17),
                ("Jefferson", "Mena", PlayerPosition.Defender, 18),
            },
            // 10. Once Caldas
            new[] {
                ("Gerardo", "Ortiz", PlayerPosition.Goalkeeper, 1),
                ("Edisson", "Palomino", PlayerPosition.Defender, 3),
                ("Sebastián", "Gómez", PlayerPosition.Midfielder, 5),
                ("Dayro", "Moreno", PlayerPosition.Forward, 9),
                ("Eder", "Chaux", PlayerPosition.Goalkeeper, 11),
                ("Jorge", "Cardona", PlayerPosition.Defender, 12),
                ("Alejandro", "García", PlayerPosition.Midfielder, 13),
                ("Billy", "Arce", PlayerPosition.Forward, 14),
                ("Jaider", "Riquett", PlayerPosition.Defender, 15),
                ("Mateo", "García", PlayerPosition.Midfielder, 16),
                ("Michael", "Barrios", PlayerPosition.Forward, 17),
                ("Juan David", "Rodríguez", PlayerPosition.Defender, 18),
            },
            // 11. Deportivo Pasto
            new[] {
                ("Diego", "Martínez", PlayerPosition.Goalkeeper, 1),
                ("Camilo", "Ayala", PlayerPosition.Defender, 4),
                ("Ray", "Vanegas", PlayerPosition.Midfielder, 10),
                ("Jown", "Cardona", PlayerPosition.Forward, 9),
                ("Diego", "Martínez", PlayerPosition.Goalkeeper, 11),
                ("Israel", "Alba", PlayerPosition.Defender, 12),
                ("Kevin", "Rendón", PlayerPosition.Midfielder, 13),
                ("Facundo", "Boné", PlayerPosition.Forward, 14),
                ("Jean", "Pestaña", PlayerPosition.Defender, 15),
                ("Adrián", "Estacio", PlayerPosition.Midfielder, 16),
                ("Gustavo", "Torres", PlayerPosition.Forward, 17),
                ("Juan", "Castilla", PlayerPosition.Defender, 18),
            },
            // 12. Deportivo Pereira
            new[] {
                ("Harlen", "Castillo", PlayerPosition.Goalkeeper, 1),
                ("David", "González", PlayerPosition.Defender, 3),
                ("Brayan", "León", PlayerPosition.Midfielder, 8),
                ("Jonier", "Mosquera", PlayerPosition.Forward, 9),
                ("Santiago", "Castaño", PlayerPosition.Goalkeeper, 11),
                ("Carlos", "Ramírez", PlayerPosition.Defender, 12),
                ("Yesus", "Cabrera", PlayerPosition.Midfielder, 13),
                ("Ángelo", "Rodríguez", PlayerPosition.Forward, 14),
                ("Jhonny", "Vásquez", PlayerPosition.Defender, 15),
                ("Andrés", "Ibargüen", PlayerPosition.Midfielder, 16),
                ("Darwin", "Quintero", PlayerPosition.Forward, 17),
                ("Yilmar", "Velásquez", PlayerPosition.Defender, 18),
            },
            // 13. Águilas Doradas
            new[] {
                ("José Fernando", "Cuadrado", PlayerPosition.Goalkeeper, 1),
                ("Éder", "Chaux", PlayerPosition.Defender, 4),
                ("Juan Pablo", "Ramírez", PlayerPosition.Midfielder, 10),
                ("Cristian", "Subero", PlayerPosition.Forward, 9),
                ("Juan", "Valencia", PlayerPosition.Goalkeeper, 11),
                ("Guillermo", "Celis", PlayerPosition.Defender, 12),
                ("Jesús", "Rivas", PlayerPosition.Midfielder, 13),
                ("Marco", "Pérez", PlayerPosition.Forward, 14),
                ("Diego", "Hernández", PlayerPosition.Defender, 15),
                ("Fredy", "Salazar", PlayerPosition.Midfielder, 16),
                ("Wilson", "Morelo", PlayerPosition.Forward, 17),
                ("Mateo", "Puerta", PlayerPosition.Defender, 18),
            },
            // 14. Boyacá Chicó FC
            new[] {
                ("Ernesto", "Hernández", PlayerPosition.Goalkeeper, 1),
                ("Carlos", "Henao", PlayerPosition.Defender, 3),
                ("Brayan", "Moreno", PlayerPosition.Midfielder, 8),
                ("Juan David", "Valencia", PlayerPosition.Forward, 9),
                ("Roger", "io", PlayerPosition.Goalkeeper, 11),
                ("Henry", "Plazas", PlayerPosition.Defender, 12),
                ("Frank", "Lozano", PlayerPosition.Midfielder, 13),
                ("Wilmar", "Cruz", PlayerPosition.Forward, 14),
                ("Kevin", "Londoño", PlayerPosition.Defender, 15),
                ("Juan", "Pérez", PlayerPosition.Midfielder, 16),
                ("Misael", "Martínez", PlayerPosition.Forward, 17),
                ("Andrés", "Correa", PlayerPosition.Defender, 18),
            },
            // 15. Jaguares de Córdoba
            new[] {
                ("Diego", "Novoa", PlayerPosition.Goalkeeper, 1),
                ("Geovan", "Montes", PlayerPosition.Defender, 4),
                ("Larry", "Vásquez", PlayerPosition.Midfielder, 5),
                ("Pablo", "Bueno", PlayerPosition.Forward, 9),
                ("Pablo", "Mina", PlayerPosition.Goalkeeper, 11),
                ("Andrés", "Rentería", PlayerPosition.Defender, 12),
                ("Juan", "Camilo", PlayerPosition.Midfielder, 13),
                ("Wilson", "Mena", PlayerPosition.Forward, 14),
                ("Yulián", "Gómez", PlayerPosition.Defender, 15),
                ("Kevin", "Padilla", PlayerPosition.Midfielder, 16),
                ("Kahiser", "Lenis", PlayerPosition.Forward, 17),
                ("Carlos", "Terán", PlayerPosition.Defender, 18),
            },
            // 16. Alianza Valledupar FC
            new[] {
                ("Luis", "Delgado", PlayerPosition.Goalkeeper, 1),
                ("Marvin", "Vallecilla", PlayerPosition.Defender, 3),
                ("Juan", "Sánchez", PlayerPosition.Midfielder, 8),
                ("Jeison", "Medina", PlayerPosition.Forward, 9),
                ("Sebastián", "Viera", PlayerPosition.Goalkeeper, 11),
                ("Pedro", "Franco", PlayerPosition.Defender, 12),
                ("Mayer", "Gil", PlayerPosition.Midfielder, 13),
                ("Róger", "Torres", PlayerPosition.Forward, 14),
                ("Jesús", "Figueroa", PlayerPosition.Defender, 15),
                ("Edwin", "Torres", PlayerPosition.Midfielder, 16),
                ("Wiston", "Fernández", PlayerPosition.Forward, 17),
                ("Jonathan", "Lopera", PlayerPosition.Defender, 18),
            },
            // 17. Fortaleza FC
            new[] {
                ("Carlos", "Mosquera", PlayerPosition.Goalkeeper, 1),
                ("Nicolás", "Giraldo", PlayerPosition.Defender, 4),
                ("Jhonier", "Viveros", PlayerPosition.Midfielder, 10),
                ("Óscar", "Vanegas", PlayerPosition.Forward, 9),
                ("Cristian", "Campestrini", PlayerPosition.Goalkeeper, 11),
                ("Hayen", "Palacios", PlayerPosition.Defender, 12),
                ("Luis", "Sánchez", PlayerPosition.Midfielder, 13),
                ("Emilio", "Arango", PlayerPosition.Forward, 14),
                ("Daniel", "Rivera", PlayerPosition.Defender, 15),
                ("Santiago", "Córdoba", PlayerPosition.Midfielder, 16),
                ("Juan José", "Ramírez", PlayerPosition.Forward, 17),
                ("Kevin", "Albalá", PlayerPosition.Defender, 18),
            },
            // 18. Llaneros FC
            new[] {
                ("José Huber", "Escobar", PlayerPosition.Goalkeeper, 1),
                ("Cristian", "Arrieta", PlayerPosition.Defender, 3),
                ("Jhon", "Pajoy", PlayerPosition.Midfielder, 8),
                ("Brayan", "Gil", PlayerPosition.Forward, 9),
                ("Kevin", "Armesto", PlayerPosition.Goalkeeper, 11),
                ("Andrés", "Cadena", PlayerPosition.Defender, 12),
                ("Michael", "Rangel", PlayerPosition.Midfielder, 13),
                ("Cristian", "Martínez", PlayerPosition.Forward, 14),
                ("Brayan", "Moreno", PlayerPosition.Defender, 15),
                ("Juan", "Zapata", PlayerPosition.Midfielder, 16),
                ("Jhony", "Cano", PlayerPosition.Forward, 17),
                ("Cristian", "Valencia", PlayerPosition.Defender, 18),
            },
            // 19. Cúcuta Deportivo
            new[] {
                ("Norberto", "Araujo", PlayerPosition.Goalkeeper, 1),
                ("Jefry", "Díaz", PlayerPosition.Defender, 4),
                ("Juan Camilo", "Portilla", PlayerPosition.Midfielder, 10),
                ("Edwar", "López", PlayerPosition.Forward, 9),
                ("Miguel", "Parra", PlayerPosition.Goalkeeper, 11),
                ("Mauricio", "Duarte", PlayerPosition.Defender, 12),
                ("Cristian", "Mosquera", PlayerPosition.Midfielder, 13),
                ("Lucas", "Ríos", PlayerPosition.Forward, 14),
                ("Jeison", "Angulo", PlayerPosition.Defender, 15),
                ("Andrés", "Peralta", PlayerPosition.Midfielder, 16),
                ("Diego", "Echeverri", PlayerPosition.Forward, 17),
                ("Brayan", "Moreno", PlayerPosition.Defender, 18),
            },
            // 20. Internacional de Bogotá
            new[] {
                ("Neto", "Volpi", PlayerPosition.Goalkeeper, 1),
                ("Nicolás", "Hernández", PlayerPosition.Defender, 3),
                ("Carlos Darwin", "Quintero", PlayerPosition.Midfielder, 10),
                ("Facundo", "Boné", PlayerPosition.Forward, 9),
                ("Cristian", "Bonilla", PlayerPosition.Goalkeeper, 11),
                ("Anderson", "Piedrahita", PlayerPosition.Defender, 12),
                ("Kevin", "Salazar", PlayerPosition.Midfielder, 13),
                ("Luis", "Peralta", PlayerPosition.Forward, 14),
                ("Brayan", "Ceballos", PlayerPosition.Defender, 15),
                ("Juan José", "Mejía", PlayerPosition.Midfielder, 16),
                ("Stiven", "Rodríguez", PlayerPosition.Forward, 17),
                ("Daniel", "Murillo", PlayerPosition.Defender, 18),
            },
            };

            var players = new List<Player>();
            for (int i = 0; i < teams.Count; i++)
            {
                foreach (var pd in playersData[i])
                {
                    players.Add(new Player
                    {
                        FirstName = pd.First,
                        LastName = pd.Last,
                        Number = pd.Number,
                        Position = pd.Pos,
                        BirthDate = new DateTime(1995, 1, 1).AddMonths(players.Count),
                        TeamId = teams[i].Id
                    });
                }
            }
            context.Players.AddRange(players);
            await context.SaveChangesAsync();

            // ═══ 3. ÁRBITROS ═══
            var referees = new List<Referee>
        {
            new() { FirstName="Wilmar", LastName="Roldán", Nationality="Colombia" },
            new() { FirstName="Andrés", LastName="Rojas", Nationality="Colombia" },
            new() { FirstName="Carlos", LastName="Betancur", Nationality="Colombia" },
            new() { FirstName="Jhon", LastName="Hinestroza", Nationality="Colombia" },
        };
            context.Referees.AddRange(referees);
            await context.SaveChangesAsync();

            // ═══ 4. TORNEO ═══
            var tournament = new Tournament
            {
                Name = "Liga BetPlay 2026-I",
                Season = "2026-I",
                StartDate = new DateTime(2026, 1, 16),
                EndDate = new DateTime(2026, 6, 5),
                Status = TournamentStatus.InProgress
            };
            context.Tournaments.Add(tournament);
            await context.SaveChangesAsync();

            // ═══ 5. INSCRIBIR LOS 20 EQUIPOS ═══
            foreach (var team in teams)
            {
                context.TournamentTeams.Add(new TournamentTeam
                {
                    TournamentId = tournament.Id,
                    TeamId = team.Id
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
