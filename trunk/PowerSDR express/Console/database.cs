//=================================================================
// database.cs
//=================================================================
// PowerSDR is a C# implementation of a Software Defined Radio.
// Copyright (C) 2004, 2005, 2006  FlexRadio Systems
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// You may contact us via email at: sales@flex-radio.com.
// Paper mail may be sent to: 
//    FlexRadio Systems
//    12100 Technology Blvd.
//    Austin, TX 78727
//    USA
//=================================================================

/*
 *  Changes for GenesisRadio
 *  Copyright (C)2008,2009,2010,2011,2012 YT7PWR Goran Radivojevic
 *  contact via email at: yt7pwr@ptt.rs or yt7pwr2002@yahoo.com
*/

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace PowerSDR
{
	class DB
	{
        public static DataSet ds;

        private static string app_data_path = "";
        public static string AppDataPath
        {
            set { app_data_path = value; }
        }

		#region Private Member Functions

		private static void Create() // changes yt7pwr
		{
			// Create the tables
			AddBandTextTable();
			AddBandStackTable();
			AddMemoryTable();
			AddGroupListTable();
            AddTXProfileDefTable();
		}

        private static void FillTables() // changes yt7pwr
		{
			// initialize tables
            FillMemory();
		}

        private static void AddBandTextTable()
        {
            ds.Tables.Add("BandText");
            DataTable t = ds.Tables["BandText"];

            t.Columns.Add("Low", typeof(double));
            t.Columns.Add("High", typeof(double));
            t.Columns.Add("Name", typeof(string));
            t.Columns.Add("TX", typeof(bool));

            object[] data = {
                                0.135700, 0.135999, "Trans atlantic window",    true,
                                0.136000, 0.137399, "CW",                       true,
                                0.137400, 0.137599, "Digital modes",            true,
                                0.137600, 0.137800, "Very slow telegraphy",     true,
                                0.415000, 0.524999, "600m band",                true,
								1.800000, 1.809999, "160M CW/Digital Modes",	true,
								1.810000, 1.810000, "160M CW QRP",				true,
								1.810001, 1.836599, "160M CW",					true,
                                1.836600, 1.836600, "160M WSPR",				true,
                                1.836601, 1.837999, "160M CW",					true,
                                1.838000, 1.838000, "160M JT65HF",  			true,
                                1.838001, 1.842999, "160M CW",					true,
								1.843000, 1.909999, "160M SSB/SSTV/Wide Band",	true,
								1.910000, 1.910000, "160M SSB QRP",				true,
								1.910001, 1.994999, "160M SSB/SSTV/Wide Band",	true,
								1.995000, 1.999999, "160M Experimental",		true,

								3.500000, 3.524999, "80M Extra CW",				true,
								3.525000, 3.575999, "80M CW",					true,
                                3.576000, 3.576000, "80M JT65HF",				true,
                                3.576001, 3.579999, "80M CW",					true,
								3.580000, 3.589999, "80M RTTY",					true,
								3.590000, 3.590000, "80M RTTY DX",				true,
								3.590001, 3.592599, "80M RTTY",					true,
                                3.592600, 3.592600, "80M WSPR",					true,
                                3.592601, 3.599999, "80M RTTY",					true,
								3.600000, 3.699999, "75M Extra SSB",			true,
								3.700000, 3.789999, "75M Ext/Adv SSB",			true,
								3.790000, 3.799999, "75M Ext/Adv DX Window",	true,
								3.800000, 3.844999, "75M SSB",					true,
								3.845000, 3.845000, "75M SSTV",					true,
								3.845001, 3.884999, "75M SSB",					true,
								3.885000, 3.885000, "75M AM Calling Frequency", true,
								3.885001, 3.999999, "75M SSB",					true,

                                5.287200, 5.287200, "60M WSPR",     			true,
								5.330500, 5.330500, "60M Channel 1",			true,
								5.346500, 5.346500, "60M Channel 2",			true,
								5.366500, 5.366500, "60M Channel 3",			true,
								5.371500, 5.371500, "60M Channel 4",			true,
								5.403500, 5.403500, "60M Channel 5",			true,
								
								7.000000, 7.024999, "40M Extra CW",				true,
								7.025000, 7.038599, "40M CW",					true,
                                7.038600, 7.038600, "40M WSPR",					true,
                                7.038601, 7.039999, "40M CW",					true,
								7.040000, 7.040000, "40M RTTY DX",				true,
								7.040001, 7.075999, "40M RTTY",					true,
                                7.076000, 7.076000, "40M JT65HF",				true,
                                7.076001, 7.099999, "40M RTTY",					true,
								7.100000, 7.124999, "40M CW",					true,
								7.125000, 7.170999, "40M Ext/Adv SSB",			true,
								7.171000, 7.171000, "40M SSTV",					true,
								7.171001, 7.174999, "40M Ext/Adv SSB",			true,
								7.175000, 7.289999, "40M SSB",					true,
								7.290000, 7.290000, "40M AM Calling Frequency", true,
								7.290001, 7.299999, "40M SSB",					true,
								
								10.100000, 10.129999, "30M CW",					true,
								10.130000, 10.138699, "30M RTTY",				true,
                                10.138700, 10.138700, "30M WSPR",				true,
                                10.138701, 10.139999, "30M RTTY",				true,
								10.140000, 10.149999, "30M Packet",				true,

								14.000000, 14.024999, "20M Extra CW",			true,
								14.025000, 14.069999, "20M CW",					true,
								14.070000, 14.075999, "20M RTTY",				true,
                                14.076000, 14.076000, "20M JT65HF",				true,
                                14.076001, 14.094999, "20M RTTY",				true,
								14.095000, 14.095599, "20M Packet",				true,
                                14.095600, 14.095600, "20M WSPR",				true,
                                14.095601, 14.099499, "20M Packet",				true,
								14.099500, 14.099999, "20M CW",					true,
								14.100000, 14.100000, "20M NCDXF Beacons",		true,
								14.100001, 14.100499, "20M CW",					true,
								14.100500, 14.111999, "20M Packet",				true,
								14.112000, 14.149999, "20M CW",					true,
								14.150000, 14.174999, "20M Extra SSB",			true,
								14.175000, 14.224999, "20M Ext/Adv SSB",		true,
								14.225000, 14.229999, "20M SSB",				true,
								14.230000, 14.230000, "20M SSTV",				true,
								14.230001, 14.285999, "20M SSB",				true,
								14.286000, 14.286000, "20M AM Calling Frequency", true,
								14.286001, 14.349999, "20M SSB",				true,
								
								18.068000, 18.099999, "17M CW",					true,
								18.100000, 18.104599, "17M RTTY",				true,
                                18.104600, 18.104600, "17M WSPR",				true,
                                18.104601, 18.104999, "17M RTTY",				true,
								18.105000, 18.109999, "17M Packet",				true,
								18.110000, 18.110000, "17M NCDXF Beacons",		true,
								18.110001, 18.167999, "17M SSB",				true,
								
								21.000000, 21.024999, "15M Extra CW",			true,
								21.025000, 21.069999, "15M CW",					true,
								21.070000, 21.075999, "15M RTTY",				true,
                                21.076000, 21.076000, "15M JT65HF",				true,
                                21.076001, 21.094599, "15M RTTY",				true,
                                21.094600, 21.094600, "15M WSPR",				true,
                                21.094601, 21.099999, "15M RTTY",				true,
								21.100000, 21.109999, "15M Packet",				true,
								21.110000, 21.149999, "15M CW",					true,
								21.150000, 21.150000, "15M NCDXF Beacons",		true,
								21.150001, 21.199999, "15M CW",					true,
								21.200000, 21.224999, "15M Extra SSB",			true,
								21.225000, 21.274999, "15M Ext/Adv SSB",		true,
								21.275000, 21.339999, "15M SSB",				true,
								21.340000, 21.340000, "15M SSTV",				true,
								21.340001, 21.449999, "15M SSB",				true,
								
								24.890000, 24.919999, "12M CW",					true,
								24.920000, 24.924599, "12M RTTY",				true,
                                24.924600, 24.924600, "12M WSPR",				true,
                                24.924601, 24.924999, "12M RTTY",				true,
								24.925000, 24.929999, "12M Packet",				true,
								24.930000, 24.930000, "12M NCDXF Beacons",		true,
								24.930001, 24.989999, "12M SSB",				true,
								
								28.000000, 28.069999, "10M CW",					true,
								28.070000, 28.075999, "10M RTTY",				true,
                                28.076000, 28.076000, "10M JT65HF",				true,
                                28.076001, 28.124599, "10M RTTY",				true,
                                28.124600, 28.124600, "10M WSPR",				true,
                                28.124601, 28.149999, "10M RTTY",				true,
								28.150000, 28.199999, "10M CW",					true,
								28.200000, 28.200000, "10M NCDXF Beacons",		true,
								28.200001, 28.299999, "10M Beacons",			true,
								28.300000, 28.679999, "10M SSB",				true,
								28.680000, 28.680000, "10M SSTV",				true,
								28.680001, 28.999999, "10M SSB",				true,
								29.000000, 29.199999, "10M AM",					true,
								29.200000, 29.299999, "10M SSB",				true,
								29.300000, 29.509999, "10M Satellite Downlinks", true,
								29.510000, 29.519999, "10M Deadband",			true,
								29.520000, 29.589999, "10M Repeater Inputs",	true,
								29.590000, 29.599999, "10M Deadband",			true,
								29.600000, 29.600000, "10M FM Simplex",			true,
								29.600001, 29.609999, "10M Deadband",			true,
								29.610000, 29.699999, "10M Repeater Outputs",	true,
								
								50.000000, 50.059999, "6M CW",					true,
								50.060000, 50.079999, "6M Beacon Sub-Band",		true,
								50.080000, 50.099999, "6M CW",					true,
								50.100000, 50.124999, "6M DX Window",			true,
								50.125000, 50.125000, "6M Calling Frequency",	true,
								50.125001, 50.292999, "6M SSB",					true,
                                50.293000, 50.293000, "6M WSPR",				true,
                                50.293001, 50.299999, "6M SSB",					true,
								50.300000, 50.599999, "6M All Modes",			true,
								50.600000, 50.619999, "6M Non Voice",			true,
								50.620000, 50.620000, "6M Digital Packet Calling", true,
								50.620001, 50.799999, "6M Non Voice",			true,
								50.800000, 50.999999, "6M RC",					true,
								51.000000, 51.099999, "6M Pacific DX Window",	true,
								51.100000, 51.119999, "6M Deadband",			true,
								51.120000, 51.179999, "6M Digital Repeater Inputs", true,
								51.180000, 51.479999, "6M Repeater Inputs",		true,
								51.480000, 51.619999, "6M Deadband",			true,
								51.620000, 51.679999, "6M Digital Repeater Outputs", true,
								51.680000, 51.979999, "6M Repeater Outputs",	true,
								51.980000, 51.999999, "6M Deadband",			true,
								52.000000, 52.019999, "6M Repeater Inputs",		true,
								52.020000, 52.020000, "6M FM Simplex",			true,
								52.020001, 52.039999, "6M Repeater Inputs",		true,
								52.040000, 52.040000, "6M FM Simplex",			true,
								52.040001, 52.479999, "6M Repeater Inputs",		true,
								52.480000, 52.499999, "6M Deadband",			true,
								52.500000, 52.524999, "6M Repeater Outputs",	true,
								52.525000, 52.525000, "6M Primary FM Simplex",	true,
								52.525001, 52.539999, "6M Deadband",			true,
								52.540000, 52.540000, "6M Secondary FM Simplex", true,
								52.540001, 52.979999, "6M Repeater Outputs",	true,
								52.980000, 52.999999, "6M Deadbands",			true,
								53.000000, 53.000000, "6M Remote Base FM Spx",	true,
								53.000001, 53.019999, "6M Repeater Inputs",		true,
								53.020000, 53.020000, "6M FM Simplex",			true,
								53.020001, 53.479999, "6M Repeater Inputs",		true,
								53.480000, 53.499999, "6M Deadband",			true,
								53.500000, 53.519999, "6M Repeater Outputs",	true,
								53.520000, 53.520000, "6M FM Simplex",			true,
								53.520001, 53.899999, "6M Repeater Outputs",	true,
								53.900000, 53.900000, "6M FM Simplex",			true,
								53.900010, 53.979999, "6M Repeater Outputs",	true,
								53.980000, 53.999999, "6M Deadband",			true,
								
                                70.000000, 70.089999, "4M CW Telegraphy",		true,
                                70.090000, 70.090499, "4M CW/Beacons",  		true,
                                70.090500, 70.091499, "4M WSPR beacons",  		true,
                                70.091500, 70.099999, "4M CW/Beacons",  		true,
                                70.100000, 70.184999, "4M CW/SSB",      		true,
                                70.185000, 70.199999, "4M Cross band calling", 	true,
                                70.200000, 70.249999, "4M CW/SSB",      		true,
                                70.250000, 70.250000, "4M MS calling",     		true,
                                70.250001, 70.259999, "4M All modes",      		true,
                                70.260000, 70.260000, "4M AM/FM calling",  		true,
                                70.260001, 70.269999, "4M All modes",      		true,
                                70.270000, 70.270000, "4M MGM activity centre",	true,
                                70.270001, 70.299999, "4M All modes",      		true,
                                70.300000, 70.300000, "4M RTTY/FAX",      		true,
                                70.300001, 70.311499, "4M FM 12.5KHz spacing",  true,
                                70.312500, 70.312500, "4M Digital",      		true,
                                70.312501, 70.324999, "4M FM 12.5KHz spacing",	true,
                                70.325000, 70.325000, "4M Digital",      		true,
                                70.322501, 70.449999, "4M FM 12.5KHz spacing",	true,
                                70.450000, 70.450000, "4M FM calling",      	true,
                                70.450001, 70.487499, "4M FM 12.5KHz spacing",	true,
                                70.487500, 70.487500, "4M Digital",         	true,
                                70.487501, 70.499999, "4M FM 12.5KHz spacing",	true,

								144.000000, 144.099999, "2M CW",				true,
								144.100000, 144.199999, "2M CW/SSB",			true,
								144.200000, 144.200000, "2M MS Calling",    	true,
								144.200001, 144.274999, "2M CW/SSB",			true,
								144.275000, 144.299999, "2M SSB Center",    	true,
								144.300000, 144.399999, "2M SSB",   			true,
                                144.400000, 144.487999, "2M Satellite",			true,
                                144.488000, 144.488000, "2M WSPR",  			true,
                                144.488001, 144.499999, "2M Satellite",			true,
								144.500000, 144.599999, "2M Linear Translator Inputs", true,
								144.600000, 144.899999, "2M FM Repeater",		true,
								144.900000, 145.199999, "2M FM Simplex",		true,
								145.200000, 145.499999, "2M FM Repeater",		true,
								145.500000, 145.799999, "2M FM Simplex",		true,
								145.800000, 145.999999, "2M Satellite",			true,
								146.000000, 146.399999, "2M FM Repeater",		true,
								146.400000, 146.609999, "2M FM Simplex",		true,
								146.610000, 147.389999, "2M FM Repeater",		true,
								147.390000, 147.599999, "2M FM Simplex",		true,
								147.600000, 147.999999, "2M FM Repeater",		true,

								222.000000, 222.024999, "1.25M EME/Weak Signal", true,
								222.025000, 222.049999, "1.25M Weak Signal",	true,
								222.050000, 222.059999, "1.25M Propagation Beacons", true,
								222.060000, 222.099999, "1.25M Weak Signal",	true,
								222.100000, 222.100000, "1.25M SSB/CW Calling",	true,
								222.100001, 222.149999, "1.25M Weak Signal CW/SSB", true,
								222.150000, 222.249999, "1.25M Local Option",	true,
								222.250000, 223.380000, "1.25M FM Repeater Inputs", true,
								222.380001, 223.399999, "1.25M General", true,
								223.400000, 223.519999, "1.25M FM Simplex",		true,
								223.520000, 223.639999, "1.25M Digital/Packet",	true,
								223.640000, 223.700000, "1.25M Links/Control",	true,
								223.700001, 223.709999, "1.25M General",	true,
								223.710000, 223.849999, "1.25M Local Option",	true,
								223.850000, 224.980000, "1.25M Repeater Outputs", true,

								420.000000, 425.999999, "70CM ATV Repeater",	true,
								426.000000, 431.999999, "70CM ATV Simplex",		true,
								432.000000, 432.069999, "70CM EME",				true,
								432.070000, 432.099999, "70CM Weak Signal CW",	true,
								432.100000, 432.100000, "70CM Calling Frequency", true,
								432.100001, 432.299999, "70CM Mixed Mode Weak Signal", true,
								432.300000, 432.399999, "70CM Propagation Beacons", true,
								432.400000, 432.999999, "70CM Mixed Mode Weak Signal", true,
								433.000000, 434.999999, "70CM Auxillary/Repeater Links", true,
								435.000000, 437.999999, "70CM Satellite Only",	true,
								438.000000, 441.999999, "70CM ATV Repeater",	true,
								442.000000, 444.999999, "70CM Local Repeaters",	true,
								445.000000, 445.999999, "70CM Local Option",	true,
								446.000000, 446.000000, "70CM Simplex",			true,
								446.000001, 446.999999, "70CM Local Option",	true,
								447.000000, 450.000000, "70CM Local Repeaters", true,

								902.000000, 902.099999, "33CM Weak Signal SSTV/FAX/ACSSB", true,
								902.100000, 902.100000, "33CM Weak Signal Calling", true,
								902.100001, 902.799999, "33CM Weak Signal SSTV/FAX/ACSSB", true,
								902.800000, 902.999999, "33CM Weak Signal EME/CW", true,
								903.000000, 903.099999, "33CM Digital Modes",	true,
								903.100000, 903.100000, "33CM Alternate Calling", true,
								903.100001, 905.999999, "33CM Digital Modes",	true,
								906.000000, 908.999999, "33CM FM Repeater Inputs", true,
								909.000000, 914.999999, "33CM ATV",				true,
								915.000000, 917.999999, "33CM Digital Modes",	true,
								918.000000, 920.999999, "33CM FM Repeater Outputs", true,
								921.000000, 926.999999, "33CM ATV",				true,
								927.000000, 928.000000, "33CM FM Simplex/Links", true,
								
								1240.000000, 1245.999999, "23CM ATV #1",		true,
								1246.000000, 1251.999999, "23CM FMN Point/Links", true,
								1252.000000, 1257.999999, "23CM ATV #2, Digital Modes", true,
								1258.000000, 1259.999999, "23CM FMN Point/Links", true,
								1260.000000, 1269.999999, "23CM Sat Uplinks/Wideband Exp.", true,
								1270.000000, 1275.999999, "23CM Repeater Inputs", true,
								1276.000000, 1281.999999, "23CM ATV #3",		true,
								1282.000000, 1287.999999, "23CM Repeater Outputs",	true,
								1288.000000, 1293.999999, "23CM Simplex ATV/Wideband Exp.", true,
								1294.000000, 1294.499999, "23CM Simplex FMN",		true,
								1294.500000, 1294.500000, "23CM FM Simplex Calling", true,
								1294.500001, 1294.999999, "23CM Simplex FMN",		true,
								1295.000000, 1295.799999, "23CM SSTV/FAX/ACSSB/Exp.", true,
								1295.800000, 1295.999999, "23CM EME/CW Expansion",	true,
								1296.000000, 1296.049999, "23CM EME Exclusive",		true,
								1296.050000, 1296.069999, "23CM Weak Signal",		true,
								1296.070000, 1296.079999, "23CM CW Beacons",		true,
								1296.080000, 1296.099999, "23CM Weak Signal",		true,
								1296.100000, 1296.100000, "23CM CW/SSB Calling",	true,
								1296.100001, 1296.399999, "23CM Weak Signal",		true,
								1296.400000, 1296.599999, "23CM X-Band Translator Input", true,
								1296.600000, 1296.799999, "23CM X-Band Translator Output", true,
								1296.800000, 1296.999999, "23CM Experimental Beacons", true,
								1297.000000, 1300.000000, "23CM Digital Modes",		true,

								2300.000000, 2302.999999, "2.3GHz High Data Rate", true,
								2303.000000, 2303.499999, "2.3GHz Packet",		true,
								2303.500000, 2303.800000, "2.3GHz TTY Packet",	true,
								2303.800001, 2303.899999, "2.3GHz General",	true,
								2303.900000, 2303.900000, "2.3GHz Packet/TTY/CW/EME", true,
								2303.900001, 2304.099999, "2.3GHz CW/EME",		true,
								2304.100000, 2304.100000, "2.3GHz Calling Frequency", true,
								2304.100001, 2304.199999, "2.3GHz CW/EME/SSB",	true,
								2304.200000, 2304.299999, "2.3GHz SSB/SSTV/FAX/Packet AM/Amtor", true,
								2304.300000, 2304.319999, "2.3GHz Propagation Beacon Network", true,
								2304.320000, 2304.399999, "2.3GHz General Propagation Beacons", true,
								2304.400000, 2304.499999, "2.3GHz SSB/SSTV/ACSSB/FAX/Packet AM", true,
								2304.500000, 2304.699999, "2.3GHz X-Band Translator Input", true,
								2304.700000, 2304.899999, "2.3GHz X-Band Translator Output", true,
								2304.900000, 2304.999999, "2.3GHz Experimental Beacons", true,
								2305.000000, 2305.199999, "2.3GHz FM Simplex", true,
								2305.200000, 2305.200000, "2.3GHz FM Simplex Calling", true,
								2305.200001, 2305.999999, "2.3GHz FM Simplex", true,
								2306.000000, 2308.999999, "2.3GHz FM Repeaters", true,
								2309.000000, 2310.000000, "2.3GHz Control/Aux Links", true,
								2390.000000, 2395.999999, "2.3GHz Fast-Scan TV", true,
								2396.000000, 2398.999999, "2.3GHz High Rate Data", true,
								2399.000000, 2399.499999, "2.3GHz Packet", true,
								2399.500000, 2399.999999, "2.3GHz Control/Aux Links", true,
								2400.000000, 2402.999999, "2.4GHz Satellite", true,
								2403.000000, 2407.999999, "2.4GHz Satellite High-Rate Data", true,
								2408.000000, 2409.999999, "2.4GHz Satellite", true,
								2410.000000, 2412.999999, "2.4GHz FM Repeaters", true,
								2413.000000, 2417.999999, "2.4GHz High-Rate Data", true,
								2418.000000, 2429.999999, "2.4GHz Fast-Scan TV", true,
								2430.000000, 2432.999999, "2.4GHz Satellite", true,
								2433.000000, 2437.999999, "2.4GHz Sat. High-Rate Data", true,
								2438.000000, 2450.000000, "2.4GHz Wideband FM/FSTV/FMTV", true,

								3456.000000, 3456.099999, "3.4GHz General", true,
								3456.100000, 3456.100000, "3.4GHz Calling Frequency", true,
								3456.100001, 3456.299999, "3.4GHz General", true,
								3456.300000, 3456.400000, "3.4GHz Propagation Beacons", true,

								5760.000000, 5760.099999, "5.7GHz General", true,
								5760.100000, 5760.100000, "5.7GHz Calling Frequency", true,
								5760.100001, 5760.299999, "5.7GHz General", true,
								5760.300000, 5760.400000, "5.7GHz Propagation Beacons", true,

								10368.000000, 10368.099999, "10GHz General", true,
								10368.100000, 10368.100000, "10GHz Calling Frequency", true,
								10368.100001, 10368.400000, "10GHz General", true,

								24192.000000, 24192.099999, "24GHz General", true,
								24192.100000, 24192.100000, "24GHz Calling Frequency", true,
								24192.100001, 24192.400000, "24GHz General", true,

								47088.000000, 47088.099999, "47GHz General", true,
								47088.100000, 47088.100000, "47GHz Calling Frequency", true,
								47088.100001, 47088.400000, "47GHz General", true,

								2.500000, 2.500000, "WWV",						false,
								5.000000, 5.000000, "WWV",						false,
								10.000000, 10.000000, "WWV",					false,
								15.000000, 15.000000, "WWV",					false,
								20.000000, 20.000000, "WWV",					false,

								0.525000, 1.710000, "Broadcast AM Med Wave",	false,				
								2.300000, 2.495000, "120M Short Wave",			false,
								3.200000, 3.400000, "90M Short Wave",			false,
								4.750000, 4.999999, "60M Short Wave",			false,
								5.000001, 5.060000, "60M Short Wave",			false,
								5.900000, 6.200000, "49M Short Wave",			false,
								7.300000, 7.350000, "41M Short Wave",			false,
								9.400000, 9.900000, "31M Short Wave",			false,
								11.600000, 12.100000, "25M Short Wave",			false,
								13.570000, 13.870000, "22M Short Wave",			false,
								15.100000, 15.800000, "19M Short Wave",			false,
								17.480000, 17.900000, "16M Short Wave",			false,
								18.900000, 19.020000, "15M Short Wave",			false,
								21.450000, 21.850000, "13M Short Wave",			false,
								25.600000, 26.100000, "11M Short Wave",			false,
			};

            for (int i = 0; i < data.Length / 4; i++)
            {
                DataRow dr = t.NewRow();
                dr["Low"] = (double)data[i * 4 + 0];
                dr["High"] = (double)data[i * 4 + 1];
                dr["Name"] = (string)data[i * 4 + 2];
                dr["TX"] = (bool)data[i * 4 + 3];
                t.Rows.Add(dr);
            }
        }

        private static void AddBandStackTable()
        {
            ds.Tables.Add("BandStack");
            DataTable t = ds.Tables["BandStack"];

            t.Columns.Add("BandName", typeof(string));
            t.Columns.Add("ModeMainRX", typeof(string));
            t.Columns.Add("ModeSubRX", typeof(string));
            t.Columns.Add("FilterMainRX", typeof(string));
            t.Columns.Add("FilterSubRX", typeof(string));
            t.Columns.Add("VFOA", typeof(double));
            t.Columns.Add("VFOB", typeof(double));
            t.Columns.Add("LOSC", typeof(double));
            t.Columns.Add("AF", typeof(int));
            t.Columns.Add("RF", typeof(int));
            t.Columns.Add("PWR", typeof(double));
            t.Columns.Add("SQL1", typeof(int));
            t.Columns.Add("SQL1_ON", typeof(bool));
            t.Columns.Add("SQL2", typeof(int));
            t.Columns.Add("SQL2_ON", typeof(bool));

            object[] data = {
								"160M", "CWU", "F8", 1.810000,
								"160M", "LSB", "F7", 1.835000,
								"160M", "LSB", "F7", 1.845000,
								"80M", "CWU", "F8", 3.540000,
								"80M", "LSB", "F7", 3.710000,
								"80M", "LSB", "F7", 3.780000,
								"60M", "USB", "F7", 5.330500,
								"60M", "USB", "F7", 5.346500,
								"60M", "USB", "F7", 5.366500,
								"60M", "USB", "F7", 5.371500,
								"60M", "USB", "F7", 5.403500,
								"40M", "CWU", "F8", 7.040000,
								"40M", "LSB", "F7", 7.110000,
								"40M", "LSB", "F7", 7.170000,
								"30M", "CWU", "F8", 10.120000,
								"30M", "USB", "F8", 10.130000,
								"30M", "USB", "F6", 10.140000,
								"20M", "CWU", "F8", 14.040000,
								"20M", "USB", "F7", 14.240000,
								"20M", "USB", "F7", 14.310000,
								"17M", "CWU", "F8", 18.090000,
								"17M", "USB", "F7", 18.125000,
								"17M", "USB", "F7", 18.140000,
								"15M", "CWU", "F8", 21.0400000,
								"15M", "USB", "F7", 21.240000,
								"15M", "USB", "F7", 21.400000,
								"12M", "CWU", "F8", 24.895000,
								"12M", "USB", "F7", 24.900000,
								"12M", "USB", "F7", 24.910000,
								"10M", "CWU", "F8", 28.040000,
								"10M", "USB", "F7", 28.470000,
								"10M", "USB", "F7", 28.540000,
								"6M", "CWU", "F8", 50.080000,
								"6M", "USB", "F7", 50.130000,
								"6M", "USB", "F7", 50.200000,								
								"2M", "CWU", "F8", 144.040000,
								"2M", "USB", "F7", 144.200000,
								"2M", "USB", "F7", 144.300000,
								"WWV", "SAM", "F7", 2.500000,
								"WWV", "SAM", "F7", 5.000000,
								"WWV", "SAM", "F7", 10.000000,
								"WWV", "SAM", "F7", 15.000000,
								"WWV", "SAM", "F7", 20.000000,
								"GEN", "SAM", "F6", 13.845000,
								"GEN", "SAM", "F7", 5.975000,
								"GEN", "SAM", "F7", 9.550000,
								"GEN", "SAM", "F8", 0.590000,
								"GEN", "SAM", "F7", 3.850000,
                                "X1", "USB", "F7", 144.00000,
                                "X1", "USB", "F7", 144.00000,
                                "X1", "USB", "F7", 144.00000,
                                "X2", "USB", "F7", 144.00000,
                                "X2", "USB", "F7", 144.00000,
                                "X2", "USB", "F7", 144.00000,
                                "X3", "USB", "F7", 144.00000,
                                "X3", "USB", "F7", 144.00000,
                                "X3", "USB", "F7", 144.00000,
                                "X4", "USB", "F7", 144.00000,
                                "X4", "USB", "F7", 144.00000,
                                "X4", "USB", "F7", 144.00000,
                                "X5", "USB", "F7", 144.00000,
                                "X5", "USB", "F7", 144.00000,
                                "X5", "USB", "F7", 144.00000,
                                "X6", "USB", "F7", 144.00000,
                                "X6", "USB", "F7", 144.00000,
                                "X6", "USB", "F7", 144.00000,
                                "X7", "USB", "F7", 144.00000,
                                "X7", "USB", "F7", 144.00000,
                                "X7", "USB", "F7", 144.00000,
                                "X8", "USB", "F7", 144.00000,
                                "X8", "USB", "F7", 144.00000,
                                "X8", "USB", "F7", 144.00000,
                                "X9", "USB", "F7", 144.00000,
                                "X9", "USB", "F7", 144.00000,
                                "X9", "USB", "F7", 144.00000,
                                "X10", "USB", "F7", 144.00000,
                                "X10", "USB", "F7", 144.00000,
                                "X10", "USB", "F7", 144.00000,
                                "X11", "USB", "F7", 144.00000,
                                "X11", "USB", "F7", 144.00000,
                                "X11", "USB", "F7", 144.00000,
                                "X12", "USB", "F7", 144.00000,
                                "X12", "USB", "F7", 144.00000,
                                "X12", "USB", "F7", 144.00000,
                                "2190M", "CWU", "F7", 0.136000,
                                "2190M", "USB", "F7", 0.136000,
                                "2190M", "USB", "F7", 0.136000,
                                "600M", "CWU", "F7", 0.50100,
                                "600M", "USB", "F7", 0.50200,
                                "600M", "USB", "F7", 0.50300,
			};

            for (int i = 0; i < data.Length / 4; i++)
            {
                DataRow dr = ds.Tables["BandStack"].NewRow();
                dr["BandName"] = (string)data[i * 4 + 0];
                dr["ModeMainRX"] = (string)data[i * 4 + 1];
                dr["ModeSubRX"] = (string)data[i * 4 + 1];
                dr["FilterMainRX"] = (string)data[i * 4 + 2];
                dr["FilterSubRX"] = (string)data[i * 4 + 2];
                dr["VFOA"] = ((double)data[i * 4 + 3]).ToString("f6");
                dr["VFOB"] = ((double)data[i * 4 + 3]).ToString("f6");
                dr["LOSC"] = ((double)data[i * 4 + 3]).ToString("f6");
                dr["AF"] = 20;
                dr["RF"] = 80;
                dr["PWR"] = 50.0;
                dr["SQL1"] = 50;
                dr["SQL1_ON"] = false;
                dr["SQL2"] = 50;
                dr["SQL2_on"] = false;
                ds.Tables["BandStack"].Rows.Add(dr);
            }
        }

        private static bool RemoveMemoryTable(string table) // changes yt7pwr
        {
            try
            {
                ds.Tables.Remove(table);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private static void AddMemoryTable() // changes yt7pwr
		{
            ds.Tables.Add("Memory");
            DataTable memoryTable = ds.Tables["Memory"];
            memoryTable.Columns.Add("MemNumber", typeof(int));
            memoryTable.Columns.Add("Frequency", typeof(double));
            memoryTable.Columns.Add("LOSC", typeof(double));
            memoryTable.Columns.Add("ModeID", typeof(int));
            memoryTable.Columns.Add("FilterID", typeof(int));
            memoryTable.Columns.Add("Step", typeof(int));
            memoryTable.Columns.Add("AGC", typeof(int));
            memoryTable.Columns.Add("Squelch", typeof(int));
            memoryTable.Columns.Add("Zoom", typeof(int));
            memoryTable.Columns.Add("Pan", typeof(int));
            memoryTable.Columns.Add("Cleared", typeof(bool));
            memoryTable.Columns.Add("Text", typeof(string));
		}

        private static void AddGroupListTable()
        {
            ds.Tables.Add("GroupList");
            DataTable t = ds.Tables["GroupList"];

            t.Columns.Add("GroupID", typeof(int));
            t.Columns.Add("GroupName", typeof(string));

            string[] vals = { "AM", "FM", "SSB", "SSTV", "CW", "PSK", "RTTY" };

            for (int i = 0; i < vals.Length; i++)
            {
                DataRow dr = t.NewRow();
                dr[0] = i;
                dr[1] = vals[i];
                t.Rows.Add(dr);
            }
        }

        private static void AddTXProfileDefTable()
        {
            ds.Tables.Add("TXProfileDef");
            DataTable t = ds.Tables["TXProfileDef"];

            t.Columns.Add("Name", typeof(string));
            t.Columns.Add("FilterLow", typeof(int));
            t.Columns.Add("FilterHigh", typeof(int));
            t.Columns.Add("TXEQNumBands", typeof(int));
            t.Columns.Add("TXEQEnabled", typeof(bool));
            t.Columns.Add("TXEQPreamp", typeof(int));
            t.Columns.Add("TXEQ1", typeof(int));
            t.Columns.Add("TXEQ2", typeof(int));
            t.Columns.Add("TXEQ3", typeof(int));
            t.Columns.Add("TXEQ4", typeof(int));
            t.Columns.Add("TXEQ5", typeof(int));
            t.Columns.Add("TXEQ6", typeof(int));
            t.Columns.Add("TXEQ7", typeof(int));
            t.Columns.Add("TXEQ8", typeof(int));
            t.Columns.Add("TXEQ9", typeof(int));
            t.Columns.Add("TXEQ10", typeof(int));
            t.Columns.Add("DXOn", typeof(bool));
            t.Columns.Add("DXLevel", typeof(int));
            t.Columns.Add("CompressorOn", typeof(bool));
            t.Columns.Add("CompressorLevel", typeof(int));
            t.Columns.Add("CompanderOn", typeof(bool));
            t.Columns.Add("CompanderLevel", typeof(int));
            t.Columns.Add("MicGain", typeof(int));
            t.Columns.Add("Lev_On", typeof(bool));
            t.Columns.Add("Lev_Slope", typeof(int));
            t.Columns.Add("Lev_MaxGain", typeof(int));
            t.Columns.Add("Lev_Attack", typeof(int));
            t.Columns.Add("Lev_Decay", typeof(int));
            t.Columns.Add("Lev_Hang", typeof(int));
            t.Columns.Add("Lev_HangThreshold", typeof(int));
            t.Columns.Add("ALC_Slope", typeof(int));
            t.Columns.Add("ALC_MaxGain", typeof(int));
            t.Columns.Add("ALC_Attack", typeof(int));
            t.Columns.Add("ALC_Decay", typeof(int));
            t.Columns.Add("ALC_Hang", typeof(int));
            t.Columns.Add("ALC_HangThreshold", typeof(int));
            t.Columns.Add("Power", typeof(int));

            #region Default

            DataRow dr = t.NewRow();
            dr["Name"] = "Default";
            dr["FilterLow"] = 200;
            dr["FilterHigh"] = 3100;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = true;
            dr["CompanderLevel"] = 2;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region Default DX

            dr = t.NewRow();
            dr["Name"] = "Default DX";
            dr["FilterLow"] = 200;
            dr["FilterHigh"] = 3100;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = true;
            dr["DXLevel"] = 5;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 2;
            dr["MicGain"] = 5;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region AM

            dr = t.NewRow();
            dr["Name"] = "AM";
            dr["FilterLow"] = 0;
            dr["FilterHigh"] = 4000;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region Conventional

            dr = t.NewRow();
            dr["Name"] = "Conventional";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3100;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region D-104

            dr = t.NewRow();
            dr["Name"] = "D-104";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3500;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = -6;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 5;
            dr["MicGain"] = 25;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region D-104+CPDR

            dr = t.NewRow();
            dr["Name"] = "D-104+CPDR";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3500;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = -6;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = true;
            dr["CompanderLevel"] = 5;
            dr["MicGain"] = 20;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region D-104+EQ

            dr = t.NewRow();
            dr["Name"] = "D-104+EQ";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3500;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = true;
            dr["TXEQPreamp"] = -6;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 5;
            dr["MicGain"] = 20;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region DX / Constest

            dr = t.NewRow();
            dr["Name"] = "DX / Contest";
            dr["FilterLow"] = 250;
            dr["FilterHigh"] = 3250;
            dr["TXEQNumBands"] = 10;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = true;
            dr["DXLevel"] = 5;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region ESSB

            dr = t.NewRow();
            dr["Name"] = "ESSB";
            dr["FilterLow"] = 50;
            dr["FilterHigh"] = 3650;
            dr["TXEQNumBands"] = 10;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = true;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 10;
            dr["Lev_On"] = false;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region HC4-5

            dr = t.NewRow();
            dr["Name"] = "HC4-5";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3100;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 5;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region HC4-5+CPDR

            dr = t.NewRow();
            dr["Name"] = "HC4-5+CPDR";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3100;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = true;
            dr["CompanderLevel"] = 5;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region PR40+W2IHY

            dr = t.NewRow();
            dr["Name"] = "PR40+W2IHY";
            dr["FilterLow"] = 50;
            dr["FilterHigh"] = 3650;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region PR40+W2IHY+CPDR

            dr = t.NewRow();
            dr["Name"] = "PR40+W2IHY+CPDR";
            dr["FilterLow"] = 50;
            dr["FilterHigh"] = 3650;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = false;
            dr["TXEQPreamp"] = 0;
            dr["TXEQ1"] = 0;
            dr["TXEQ2"] = 0;
            dr["TXEQ3"] = 0;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = true;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region PR781+EQ

            dr = t.NewRow();
            dr["Name"] = "PR781+EQ";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3200;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = true;
            dr["TXEQPreamp"] = -11;
            dr["TXEQ1"] = -6;
            dr["TXEQ2"] = 2;
            dr["TXEQ3"] = 8;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = false;
            dr["CompanderLevel"] = 3;
            dr["MicGain"] = 12;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion

            #region PR781+EQ+CPDR

            dr = t.NewRow();
            dr["Name"] = "PR781+EQ+CPDR";
            dr["FilterLow"] = 100;
            dr["FilterHigh"] = 3200;
            dr["TXEQNumBands"] = 3;
            dr["TXEQEnabled"] = true;
            dr["TXEQPreamp"] = -9;
            dr["TXEQ1"] = -8;
            dr["TXEQ2"] = 3;
            dr["TXEQ3"] = 7;
            dr["TXEQ4"] = 0;
            dr["TXEQ5"] = 0;
            dr["TXEQ6"] = 0;
            dr["TXEQ7"] = 0;
            dr["TXEQ8"] = 0;
            dr["TXEQ9"] = 0;
            dr["TXEQ10"] = 0;
            dr["DXOn"] = false;
            dr["DXLevel"] = 3;
            dr["CompanderOn"] = true;
            dr["CompanderLevel"] = 2;
            dr["MicGain"] = 10;
            dr["Lev_On"] = true;
            dr["Lev_Slope"] = 0;
            dr["Lev_MaxGain"] = 10;
            dr["Lev_Attack"] = 2;
            dr["Lev_Decay"] = 500;
            dr["Lev_Hang"] = 500;
            dr["Lev_HangThreshold"] = 0;
            dr["ALC_Slope"] = 0;
            dr["ALC_MaxGain"] = -20;
            dr["ALC_Attack"] = 2;
            dr["ALC_Decay"] = 10;
            dr["ALC_Hang"] = 500;
            dr["ALC_HangThreshold"] = 0;
            dr["Power"] = 50;
            t.Rows.Add(dr);

            #endregion
        }

        private static void FillMemory()  // yt7pwr
        {
            int i = 0;

            for (i = 1; i < 100; i++)
            {
                DataRow newRow = ds.Tables["Memory"].NewRow();
                newRow["MemNumber"] = i;
                newRow["Frequency"] = 10.1;
                newRow["LOSC"] = 10.0;
                newRow["ModeID"] = 1;
                newRow["FilterID"] = 1;
                newRow["Step"] = 1;
                newRow["AGC"] = 1;
                newRow["Squelch"] = 1;
                newRow["Zoom"] = 4;
                newRow["Pan"] = 0;
                newRow["Cleared"] = true;
                ds.Tables["Memory"].Rows.Add(newRow);
            }
        }

		private static void CheckBandTextValid()
		{
			ArrayList bad_rows = new ArrayList();

			if(ds == null) return;
			foreach(DataRow dr in ds.Tables["BandText"].Rows)
			{
				// check low freq
				string f = ((double)dr["Low"]).ToString("f6");
				f = f.Replace(",", ".");
				DataRow[] rows = ds.Tables["BandText"].Select(f+">=Low AND "+f+"<=High");
				if(rows.Length > 1)
				{
					// handle multiple entries
					if(!bad_rows.Contains(dr))
						bad_rows.Add(dr);
				}

				// check high freq
				f = ((double)dr["High"]).ToString("f6");
				f = f.Replace(",", ".");
				rows = ds.Tables["BandText"].Select(f+">=Low AND "+f+"<=High");
				if(rows.Length > 1)
				{
					// handle multiple entries
					if(!bad_rows.Contains(dr))
						bad_rows.Add(dr);
				}
			}

            foreach(DataRow dr in bad_rows)
				ds.Tables["BandText"].Rows.Remove(dr);
		}

        public static void VerifyTables()
        {
            try
            {
                if (!ds.Tables.Contains("BandText"))
                    AddBandTextTable();
                else
                {
                    ds.Tables.Remove("BandText");
                    AddBandTextTable();
                }

                if (!ds.Tables.Contains("BandStack"))
                    AddBandStackTable();
                else
                {
                    if (!CheckBandStackTable())
                    {
                        RemoveMemoryTable("BandStack");
                        AddBandStackTable();
                    }
                }

                if (!ds.Tables.Contains("Memory"))
                {
                    AddMemoryTable();
                    FillMemory();
                }
                else if (!CheckMemoryTable())
                {
                    if (RemoveMemoryTable("Memory"))
                    {
                        AddMemoryTable();
                        FillMemory();
                    }
                }

                if (ds.Tables.Contains("TXProfile"))
                    RemoveTable("TXProfile");

                if (!ds.Tables.Contains("GroupList"))
                    AddGroupListTable();

                if (!ds.Tables.Contains("TXProfileDef"))
                    AddTXProfileDefTable();
                else if (ds.Tables.Contains("TXProfileDef"))
                {
                    if (!CheckTXProfileDefTable())
                    {
                        RemoveTable("TXProfileDef");
                        AddTXProfileDefTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Database!" + ex.ToString());
            }
        }

        private static bool CheckBandStackTable()
        {
            try
            {
                ArrayList list = new ArrayList();

                DataRow[] rows = ds.Tables["BandStack"].Select();

                foreach (DataRow dr in rows)
                {
                    list.Add(dr[0].ToString() + "/" + dr[1].ToString() + "/" + dr[2].ToString()
                        + "/" + dr[3].ToString() + "/" + dr[4].ToString() + "/" + dr[5].ToString()
                        + "/" + dr[6].ToString() + "/" + dr[7].ToString() + "/" + dr[8].ToString()
                        + "/" + dr[9].ToString() + "/" + dr[10].ToString() + dr[11].ToString()
                        + "/" + dr[12].ToString() + "/" + dr[13].ToString() + dr[14].ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private static bool CheckMemoryTable()
        {
            try
            {
                ArrayList list = new ArrayList();

                DataRow[] rows = ds.Tables["Memory"].Select("'" + 1 + "' = MemNumber");

                foreach (DataRow dr in rows)
                {
                    list.Add(dr[0].ToString() + "/" + dr[1].ToString() + "/" + dr[2].ToString()
                        + "/" + dr[3].ToString() + "/" + dr[4].ToString() + "/" + dr[5].ToString()
                        + "/" + dr[6].ToString() + "/" + dr[7].ToString() + "/" + dr[8].ToString()
                        + "/" + dr[9].ToString() + "/" + dr[10].ToString() + dr[11].ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private static bool CheckTXProfileDefTable()
        {
            try
            {
                ArrayList list = new ArrayList();

                foreach (DataRow dr in DB.ds.Tables["TxProfileDef"].Rows)
                {
                    for (int i = 0; i < 37; i++)
                    {
                        list.Add(dr[i].ToString() + "/");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

		#endregion

		#region Public Member Functions
// ======================================================
// Public Member Functions 
// ======================================================

        public static void Init()
        {
            try
            {
                ds = new DataSet("Data");

                if (File.Exists(app_data_path + "\\" + "database.xml"))
                    ds.ReadXml(app_data_path + "\\" + "database.xml");
                else
                {
                    Create();
                    FillTables();
                }

                VerifyTables();

                CheckBandTextValid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Database Init!\n" + ex.ToString());
            }
        }

        public static void Update()
        {
            ds.WriteXml(app_data_path + "\\" + "database.xml", XmlWriteMode.WriteSchema);
        }

		public static void Exit()
		{
            try
            {
                Update();
                ds = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB exit error!" + ex.ToString());
            }
		}

        public static bool BandText(double freq, out string outStr)
        {
            try
            {
                outStr = "";
                string f = freq.ToString("f6");
                f = f.Replace(",", ".");
                DataRow[] rows = ds.Tables["BandText"].Select(f + ">=Low AND " + f + "<=High");
                if (rows.Length == 0)		// band not found
                {
                    outStr = "Out of Band";
                    return false;
                }
                else if (rows.Length == 1)	// found band
                {
                    outStr = ((string)rows[0]["Name"]);
                    return (bool)rows[0]["TX"];
                }
                else //if(rows.Length > 1)	// this should never happen
                {
                    MessageBox.Show("Error reading BandInfo table.", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    outStr = "Error";
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n\n\n" + e.StackTrace, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                outStr = "Error";
                return false;
            }
        }

        public static int[] GetBandStackNum(string x1, string x2, string x3, string x4, string x5,
            string x6, string x7, string x8, string x9, string x10, string x11, string x12)
		{
			string[] band_list = {"160M", "80M", "60M", "40M", "30M", "20M", "17M",
								  "15M", "12M", "10M", "6M", "2M", "WWV", "GEN",
								  x1, x2, x3, x4, x5, x6,
								  x7, x8, x9, x10, x11, x12,
								  "2190M", "600M" };

			int[] retvals = new int[band_list.Length];

			for(int i=0; i<band_list.Length; i++)
			{
				string s = band_list[i];
				DataRow[] rows = ds.Tables["BandStack"].Select("'"+s+"' = BandName");
				retvals[i] = rows.Length;
			}

			return retvals;
		}

        // changes yt7pwr
		public static bool GetBandStack(string band, int index, out string modeMainRX, out string modeSubRX, 
            out string filterMainRX, out string filterSubRX, out double freqA, out double freqB, out double losc_freq,
            out int af,out int rf,out double pwr, out int sql1, out bool sql1_on, out int sql2, out bool sql2_on)
		{
			DataRow[] rows = ds.Tables["BandStack"].Select("'"+band+"' = BandName");

			if(rows.Length == 0)
			{
				MessageBox.Show("No Entries found for Band: "+band, "No Entry Found",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				modeMainRX = "";
                modeSubRX = "";
				filterMainRX = "";
                filterSubRX = "";
				freqA = 0.0f;
                freqB = 0.0f;
                losc_freq = 0.0f;
                af = 20;
                rf = 80;
                pwr = 50.0;
                sql1 = 50;
                sql1_on = false;
                sql2 = 50;
                sql2_on = false;
				return false;
			}

			index = index % rows.Length;
			
			modeMainRX = (string)((DataRow)rows[index])["ModeMainRX"];
            modeSubRX = (string)((DataRow)rows[index])["ModeSubRX"];
			filterMainRX = (string)((DataRow)rows[index])["FilterMainRX"];
            filterSubRX = (string)((DataRow)rows[index])["FilterSubRX"];
			freqA = (double)((DataRow)rows[index])["VFOA"];
            freqB = (double)((DataRow)rows[index])["VFOB"];
            losc_freq = (double)((DataRow)rows[index])["LOSC"];
            af = (int)((DataRow)rows[index])["AF"];
            rf = (int)((DataRow)rows[index])["RF"];
            pwr = (double)((DataRow)rows[index])["PWR"];
            sql1 = (int)((DataRow)rows[index])["SQL1"];
            sql1_on = (bool)((DataRow)rows[index])["SQL1_ON"];
            sql2 = (int)((DataRow)rows[index])["SQL2"];
            sql2_on = (bool)((DataRow)rows[index])["SQL2_ON"];
			return true;
		}

        // changes yt7pwr
        public static void AddBandStack(string band, string DSPmode,
            string filter, double freq, int af, int rf, double pwr, int sql1,
            bool sql1_on, int sql2, bool sql2_on)
        {
            try
            {
                DataRow dr = ds.Tables["BandStack"].NewRow();
                dr["BandName"] = band;
                dr["ModeMainRX"] = DSPmode;
                dr["ModeSubRX"] = DSPmode;
                dr["FilterMainRX"] = filter;
                dr["FilterSubRX"] = filter;
                dr["VFOA"] = freq;
                dr["VFOB"] = freq;
                dr["LOSC"] = freq;
                dr["AF"] = af;
                dr["RF"] = rf;
                dr["PWR"] = pwr;
                dr["SQL1"] = sql1;
                dr["SQL1_ON"] = sql1_on;
                dr["SQL2"] = sql2;
                dr["SQL2_on"] = sql2_on;
                ds.Tables["BandStack"].Rows.Add(dr);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static bool RemoveTable(string table_name) // changes yt7pwr
        {
            try
            {
                ds.Tables.Remove(table_name);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private static void AddFormTable(string name)
        {
            DataTable tbl;
            tbl = ds.Tables.Add(name);
            ds.Tables[name].Columns.Add("Key", typeof(string));
            ds.Tables[name].Columns.Add("Value", typeof(string));
            ds.Tables[name].PrimaryKey = new DataColumn[] { tbl.Columns["Key"] };
        }

        public static void SaveVars(string tableName, ref ArrayList list)
        {
            if (!ds.Tables.Contains(tableName))
                AddFormTable(tableName);
            else
            {
                RemoveTable(tableName);
                AddFormTable(tableName);
            }

            list.Sort();

            foreach (string s in list)
            {
                string[] vals = s.Split('/');

                if (vals.Length > 2)
                {
                    for (int i = 2; i < vals.Length; i++)
                        vals[1] += "/" + vals[i];
                }

                DataRow[] rows = ds.Tables[tableName].Select("Key = '" + vals[0] + "'");

                if (rows.Length == 0)	// name is not in list
                {
                    DataRow newRow = ds.Tables[tableName].NewRow();
                    newRow[0] = vals[0];
                    newRow[1] = vals[1];
                    ds.Tables[tableName].Rows.Add(newRow);
                }
                else if (rows.Length == 1)
                {
                    rows[0][1] = vals[1];
                }
            }
        }

        public static ArrayList GetVars(string tableName)
        {
            ArrayList list = new ArrayList();
            if (!ds.Tables.Contains(tableName))
                return list;

            DataTable t = ds.Tables[tableName];

            for (int i = 0; i < t.Rows.Count; i++)
            {
                list.Add(t.Rows[i][0].ToString() + "/" + t.Rows[i][1].ToString());
            }

            return list;
        }

        // changes yt7pwr
        public static void SaveBandStack(string band, int index, string modeMainRX, string modeSubRX,
            string filterMainRX, string filterSubRX, double VFOA, double VFOB, double losc_freq,
            int af, int rf, double pwr, int sql1, bool sql1_on, int sql2, bool sql2_on)
        {
            try
            {
                DataRow[] rows = ds.Tables["BandStack"].Select("'" + band + "' = BandName");

                foreach (DataRow datarow in rows)			// prevent duplicates
                {
                    if ((string)datarow["BandName"] == band && (double)datarow["VFOA"] == VFOA)
                    {
                        datarow["FilterMainRX"] = filterMainRX;
                        datarow["FilterSubRX"] = filterSubRX;
                        datarow["ModeMainRX"] = modeMainRX;
                        datarow["ModeSubRX"] = modeSubRX;
                        datarow["VFOB"] = VFOB;
                        datarow["LOSC"] = losc_freq;
                        datarow["AF"] = af;
                        datarow["RF"] = rf;
                        datarow["PWR"] = pwr;
                        datarow["SQL1"] = sql1;
                        datarow["SQL1_ON"] = sql1_on;
                        datarow["SQL2"] = sql2;
                        datarow["SQL2_on"] = sql2_on;
                        return;
                    }
                }

                index = index % rows.Length;

                DataRow d = (DataRow)rows[index];
                d["ModeMainRX"] = modeMainRX;
                d["ModeSubRX"] = modeSubRX;
                d["FilterMainRX"] = filterMainRX;
                d["FilterSubRX"] = filterSubRX;
                d["VFOA"] = VFOA;
                d["VFOB"] = VFOB;
                d["LOSC"] = losc_freq;
                d["AF"] = af;
                d["RF"] = rf;
                d["PWR"] = pwr;
                d["SQL1"] = sql1;
                d["SQL1_ON"] = sql1_on;
                d["SQL2"] = sql2;
                d["SQL2_on"] = sql2_on;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Eror!" + ex.Message);
            }
        }

        public static void ClearMemoryTable() // yt7pwr
        {
            try
            {
                DataRow[] rows = ds.Tables["Memory"].Select();

                foreach (DataRow datarow in rows)
                {
                    datarow["Frequency"] = 10.1;
                    datarow["LOSC"] = 10.0;
                    datarow["ModeID"] = 1;
                    datarow["FilterID"] = 1;
                    datarow["FilterID"] = 1;
                    datarow["Step"] = 1;
                    datarow["AGC"] = 1;
                    datarow["Squelch"] = 100;
                    datarow["Zoom"] = 4;
                    datarow["Pan"] = 0;
                    datarow["Cleared"] = true;
                    datarow["Text"] = "";
                }
            }
            catch (Exception ex)
            {
                if (!ds.Tables.Contains("Memory"))
                {
                    AddMemoryTable();
                    FillMemory();
                }
                if (!CheckMemoryTable())
                {
                    if (RemoveMemoryTable("Memory"))
                    {
                        AddMemoryTable();
                        FillMemory();
                    }
                }

                Debug.Write(ex.ToString());
            }
        }

        public static bool ClearMemory(int mem_number) // yt7pwr
        {
            try
            {
                DataRow[] rows = ds.Tables["Memory"].Select("'" + mem_number + "' = MemNumber");
                foreach (DataRow datarow in rows)
                {
                    datarow["Cleared"] = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror!" + ex.Message);
                return false;
            }
        }

        // yt7pwr
        public static bool SaveMemory(int mem_number, double freq, double losc, int mode, int filter, 
            int step, int agc, int squelch, int zoom, int pan, string text)
        {
            try
            {
                DataRow[] rows = ds.Tables["Memory"].Select("'" + mem_number + "' = MemNumber");

                foreach (DataRow datarow in rows)
                {
                    if ((int)datarow["MemNumber"] == mem_number)
                    {
                        datarow["Frequency"] = freq;
                        datarow["LOSC"] = losc;
                        datarow["ModeID"] = mode;
                        datarow["FilterID"] = filter;
                        datarow["Step"] = step;
                        datarow["AGC"] = agc;
                        datarow["Squelch"] = squelch;
                        datarow["Zoom"] = zoom;
                        datarow["Pan"] = pan;
                        datarow["Cleared"] = false;
                        datarow["Text"] = text;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!ds.Tables.Contains("Memory"))
                {
                    AddMemoryTable();
                    FillMemory();
                }
                else if (!CheckMemoryTable())
                {
                    if (RemoveMemoryTable("Memory"))
                    {
                        AddMemoryTable();
                        FillMemory();
                    }
                }

                Debug.Write(ex.ToString());

                return false;
            }
        }

        public static ArrayList GetTX_Phase_Gain() // yt7pwr
        {
            ArrayList list = new ArrayList();

            foreach (DataRow dr in ds.Tables["TXPhaseGain"].Rows)
            {
                list.Add(dr[0].ToString() + "/" + dr[1].ToString() +
                    "/" + dr[2].ToString() + "/" + dr[3].ToString());
            }

            return list;
        }

        public static ArrayList GetMemory(int mem_number) // yt7pwr
        {
            try
            {
                ArrayList list = new ArrayList();

                DataRow[] rows = ds.Tables["Memory"].Select("'" + mem_number + "' = MemNumber");

                foreach (DataRow dr in rows)
                {
                    list.Add(dr[0].ToString() + "/" + dr[1].ToString() + "/" + dr[2].ToString()
                        + "/" + dr[3].ToString() + "/" + dr[4].ToString() + "/" + dr[5].ToString()
                        + "/" + dr[6].ToString() + "/" + dr[7].ToString() + "/" + dr[8].ToString()
                        + "/" + dr[9].ToString() + "/" + dr[10].ToString() + "/" + dr[11].ToString());
                }

                return list;
            }
            catch (Exception ex)
            {
                if (!ds.Tables.Contains("Memory"))
                {
                    AddMemoryTable();
                    FillMemory();
                }
                else if (!CheckMemoryTable())
                {
                    if (RemoveMemoryTable("Memory"))
                    {
                        AddMemoryTable();
                        FillMemory();
                    }
                }

                Debug.Write(ex.ToString());

                return null;
            }
        }

        public static bool ImportDatabase(string filename)
        {
            if (!File.Exists(filename)) return false;

            DataSet file = new DataSet();

            try
            {
                file.ReadXml(filename);
            }
            catch (Exception)
            {
                return false;
            }

            ds = file;
            return true;
        }

		#endregion
	}
}
