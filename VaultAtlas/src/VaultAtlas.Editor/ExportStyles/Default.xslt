<?xml version="1.0" encoding="UTF-8" ?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html><head>
  <style><xsl:value-of select="/VaultAtlas/Settings[Name='Stylesheet']/Value"></xsl:value-of></style>
  <xsl:variable name="linkbase" select="/VaultAtlas/Settings[Name='Linkbase']/Value"></xsl:variable>
  <BASE HREF="{$linkbase}"></BASE>
  </head>
  
  <body>

<h2><xsl:value-of select="@ListName"/></h2>
    <table border="1" cellpadding="2" cellspacing="0" style="border-color: #cecece; border-collapse: collapse;">

<xsl:for-each select="/VaultAtlas/IncludedArtistsSet/IncludedArtists">

<xsl:variable name="Artistname" select="DisplayName" />

<TR><TD COLSPAN="6" CLASS="ArtistTitleCell" style="border-width: 0px 0px 1px 0px;" ><SPAN CLASS="ArtistTitle"><xsl:value-of select="$Artistname" /></SPAN> <SPAN CLASS="NumberOfShows">(<xsl:value-of select="ShowCount"/> entries)</SPAN></TD></TR>

<xsl:for-each select="/VaultAtlas/Shows[ArtistDisplay= $Artistname]">

       	<TR >
       	<TD width="100"><xsl:value-of select="DateDisplay" /></TD>
       	<TD><xsl:value-of select="City" /></TD>
       	<TD><xsl:value-of select="Venue" /></TD>
       	<TD style="text-align: right;"><xsl:value-of select="Length" /></TD>
       	<TD WIDTH="100"><xsl:if test="IsMaster='True'"><span style="color: #ff0000; padding-left: 10px;">*master*</span></xsl:if><xsl:if test="IsVideo='True'"><span style="color: #66aa00; padding-left: 10px;">*video*</span></xsl:if><xsl:if test="IsPublic='False'"><span style="color: #555555; padding-left: 10px;">*private*</span></xsl:if><xsl:if test="NeedReplacement='True'"><span style="color: #ff0000; padding-left: 10px;">*replacement*</span></xsl:if></TD>
       	<TD><xsl:value-of select="Source" /></TD>
       	</TR>

      <xsl:if test="Comments">
      	<tr>
      	<td></td>
      	<td colspan="5" class="Comments"><xsl:value-of select="Comments" /></td>
      	</tr>
      </xsl:if>
 </xsl:for-each> 
 </xsl:for-each>
 
    </table>
    	</body></html>

</xsl:template>
</xsl:transform>