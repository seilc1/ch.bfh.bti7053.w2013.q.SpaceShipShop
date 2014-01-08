<table id="attributeList">
	<tr>
		<th>Id</th>
		<th>Name</th>
		<th>Default Display</th>
		<th>Default Description</th>
		<th>Category</th>
		<th>Searchable?</th>
		<th>Action</th>
	</tr>
	<!-- ko foreach: Attributes -->
			
	<tr data-bind="ifnot: IsActive">
		<td data-bind="text: Data.Id"></td>
		<td data-bind="text: Data.TextKey"></td>
		<td data-bind="text: Data.DefaultText"></td>
		<td data-bind="text: Data.DefaultDescription"></td>
		<td data-bind="text: $parent.ResolveCategory(Data.CategoryId).TextKey"></td>
		<td data-bind="text: Data.Searchable"></td>
		<td>
			<span class="glyphicon glyphicon-cog" data-bind="click: $parent.SetActive"></span>
			<span class="glyphicon glyphicon-remove" data-bind="click: $parent.Delete"></span>
		</td>
	</tr>
				
	<tr data-bind="if: IsActive">
		<td data-bind="text: Data.Id"></td>
		<td>
			<input class="form-control" data-bind="value: Data.TextKey" />
		</td>
		<td>
			<input class="form-control" data-bind="value: Data.DefaultText" />
		</td>
		<td>
			<input class="form-control" data-bind="value: Data.DefaultDescription" />
		</td>
		<td>
			<select data-bind="options: $parent.Categories, optionsText: 'TextKey', optionsValue: 'Id', value: Data.CategoryId"></select>
		</td>
		<td>
			<input type="checkbox" data-bind="checked: Data.Searchable" />
		</td>
		<td>
			<span 
				class="glyphicon glyphicon-floppy-save"
				data-bind="click: $parent.SetInActive"></span>
			<span class="glyphicon glyphicon-remove" data-bind="click: $parent.Delete"></span>
		</td>
	</tr>
				

	<!-- /ko -->
			
	<tr>
		<td> New:</td>
		<td>
			<input class="form-control" data-bind="value: NewNode().TextKey" />
		</td>
		<td>
			<input class="form-control" data-bind="value: NewNode().DefaultText" />
		</td>
		<td>
			<input class="form-control" data-bind="value: NewNode().DefaultDescription" />
		</td>
		<td>
			<select data-bind="options: Categories, optionsText: 'TextKey', optionsValue: 'Id', value: NewNode().CategoryId"></select>
		</td>
		<td>
			<input type="checkbox" data-bind="checked: NewNode().Searchable" />
		</td>
		<td>
			<span 
				class="glyphicon glyphicon-floppy-save"
				data-bind="click: SaveNew"></span>
		</td>
	</tr>
</table>