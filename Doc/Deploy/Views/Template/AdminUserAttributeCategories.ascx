<table id="categoryList">
		<tr>
			<th>Id</th>
			<th>Name</th>
			<th>Action</th>
		</tr>
		<!-- ko foreach: Categories -->
				
		<tr data-bind="if: IsActive">
			<td data-bind="text: Id"></td>
			<td>
				<input data-bind="value: TextKey, enterkey: $parent.SetInActive" />
			</td>
			<td>
				<span 
					class="glyphicon glyphicon-floppy-save"
					data-bind="click: $parent.SetInActive"></span>
				<span class="glyphicon glyphicon-remove" data-bind="click: $parent.Delete"></span>
			</td>
		</tr>

		<tr data-bind="ifnot: IsActive">
			<td data-bind="text: Id"></td>
			<td data-bind="text: TextKey"></td>
			<td>
				<span 
					class="glyphicon glyphicon-cog"
					data-bind="click: $parent.SetActive"></span>
				<span class="glyphicon glyphicon-remove" data-bind="click: $parent.Delete"></span>
			</td>
		</tr>
		<!-- /ko -->
				
		<tr>
			<td> New:</td>
			<td>
				<input data-bind="value: NewNode().TextKey, enterkey: SaveNew" />
			</td>
			<td>
				<span 
					class="glyphicon glyphicon-floppy-save"
					data-bind="click: SaveNew"></span>
			</td>
		</tr>
	</table>