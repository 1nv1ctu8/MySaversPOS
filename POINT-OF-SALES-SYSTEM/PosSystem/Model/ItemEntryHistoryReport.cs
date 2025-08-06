using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace PosSystem.Model
{
    public class ItemEntryHistoryReport : ItemEntryHistory
    {
        public string Description { get; set; } = string.Empty;
    }

//SELECT
//    h.date_time AS `DateTime`,
//    h.item_code AS ItemCode,
//    i.DESCRIPTION AS Description,
//    h.material_cost AS MaterialCost,
//    h.price AS Price,
//    h.quantity AS Quantity,
//    u.user_name AS UserName,
//    h.remarks AS Remarks
//FROM
//    ((Item_Entry_History h
//        INNER JOIN Items i ON h.item_code = i.ITEMCODE)
//        INNER JOIN Users u ON h.updated_by = u.ID)
//ORDER BY
//    date_time
}
