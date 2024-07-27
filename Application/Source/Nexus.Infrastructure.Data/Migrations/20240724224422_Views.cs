using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nexus.Infrastructure.Data.Migrations;

[DbContext(typeof(DatabaseContext))]
[Migration("20240724224422_Views")]
public partial class Views : ViewsMigration
{ }