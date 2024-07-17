import pandas as pd
import matplotlib.pyplot as plt

# Create DataFrame from the provided data
data = {
    'Incidents': ['Data', 'Information', 'System/Service', 'Account access', 'Performance', 'Transaction process', 'Application', 'Project'],
    'SAS': [57, 54, 69, 3, 11, 4, 5, 0],
    'Qlik': [282, 14, 72, 15, 25, 5, 7, 1],
    'Total': [343, 68, 141, 18, 36, 9, 12, 1]
}

df = pd.DataFrame(data)

# Stacked Bar Chart
fig, ax = plt.subplots(figsize=(12, 6))
df.plot(kind='bar', x='Incidents', y=['SAS', 'Qlik'], stacked=True, ax=ax, color=['#1f77b4', '#ff7f0e'])
plt.title('Stacked Bar Chart of Incidents by SAS and Qlik')
plt.xlabel('Incidents')
plt.ylabel('Number of Incidents')
plt.xticks(rotation=45)
plt.legend(title='System')
plt.tight_layout()

# Save the plot
plt.savefig('stacked_bar_chart.png')

# Display the plot
plt.show()

# Pie Chart with improved text positioning
fig, ax = plt.subplots(figsize=(8, 8))
wedges, texts, autotexts = ax.pie(df['Total'], labels=df['Incidents'], autopct='%1.1f%%', startangle=140, colors=plt.cm.Paired.colors, pctdistance=0.85)

# Improve text positioning
for autotext in autotexts:
    autotext.set_color('white')
    autotext.set_weight('bold')

plt.title('Pie Chart of Total Incidents by Category')
plt.tight_layout()

# Save the plot
plt.savefig('pie_chart.png')

# Display the plot
plt.show()
